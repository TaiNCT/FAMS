using AutoMapper;
using Contracts.UserManagement;
using Entities.Context;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using UserManagementAPI.Models;
using UserManagementAPI.Models.DTO;
using UserManagementAPI.Models.DTO.ValidationDTO;
using UserManagementAPI.Services;
using UserManagementAPI.Utils;

namespace UserManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationAPIController : Controller
    {
        private readonly FamsContext _db;
        private readonly UserResponse _response;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthenticationAPIController(
            FamsContext db,
            IMapper mapper,
            IConfiguration configuration,
            IEmailService emailService,
            IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _response = new UserResponse();
            _mapper = mapper;
            _configuration = configuration;
            _emailService = emailService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("login")]
        public ActionResult<ResponseDTO> LoginUser([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var user_ = _db.Users.FirstOrDefault(u => u.Username == loginDTO.username);
                if (user_ == null)
                {
                    return new ResponseDTO
                    {

                        IsSuccess = false,
                        Message = "Cannot found the user."
                    };
                }

                if (user_.Password == loginDTO.password)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "OK"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The password is not match."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Login failed: " + ex.Message
                };
            }
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendEmail([FromBody] EmailValidationDTO emailDTO)
        {
            string email = emailDTO.Email;
            var exists = _db.Users.Any(u => u.Email == email);
            if (!exists)
            {
                return NotFound(new { Message = "Email does not match any accounts." });
            }
            string subject = "[FAMS] REQUEST TO CHANGE PASSWORD";
            string code = MyUtils.GenerateRandomCode(6);
            string content = $@"
            <html>
                <body>
                    <p>Your recovery code is: <strong style='color: red;'>{code}</strong></p>
                    <p>Please use this code to proceed with your password recovery.</p>
                    <p><i>Note: The code is valid for 5 minutes.</i></p>
                </body>
            </html>";
            var expiredTime = DateTime.UtcNow.AddMinutes(5);

            var jsonObject = new
            {
                email = email,
                code = code,
                expired_time = expiredTime
            };
            var encoder = new AesEncryption("Huhuhutoikhongmuoncodenua");
            var jsonString = System.Text.Json.JsonSerializer.Serialize(jsonObject);
            var encryptedToken = encoder.Encrypt(jsonString);
            await _emailService.SendEmailAsync(emailDTO.Email, subject, content);
            return Ok(new { fams_token = encryptedToken });
        }

        [HttpPost("verify-code")]
        public IActionResult VerifyCode([FromBody] RecoverCodeDTO codeDTO)
        {
            var encoder = new AesEncryption("Huhuhutoikhongmuoncodenua");

            string decryptedBytes = encoder.Decrypt(codeDTO.Token);

            try
            {
                JObject jsonObject = JObject.Parse(decryptedBytes);

                var email = jsonObject["email"]?.ToString();
                var code = jsonObject["code"]?.ToString();

                if (jsonObject["expired_time"] is JToken expiredTimeToken && expiredTimeToken.Type != JTokenType.Null)
                {
                    DateTime expiredTime;
                    try
                    {
                        expiredTime = expiredTimeToken.ToObject<DateTime>();
                    }
                    catch (Exception)
                    {
                        return BadRequest(new { Message = "Invalid expiration time format." });
                    }
                    if (email == null || code == null)
                    {
                        return BadRequest(new { Message = "Missing email or code in the token." });
                    }

                    if (DateTime.UtcNow > expiredTime)
                    {
                        return BadRequest(new { Message = "Token has expired." });
                    }

                    if (codeDTO.Email != email || codeDTO.Code != code)
                    {
                        return Forbid();
                    }
                    if (DateTime.UtcNow > expiredTime)
                    {
                        return BadRequest(new { Message = "Token has expired" });
                    }
                    return Ok(new { Message = "Ok" });
                }
                else
                {
                    return BadRequest(new { Message = "Expiration time is missing or null." });
                }
            }
            catch
            {
                return BadRequest(new { Message = "Error" });
            }
        }

        [HttpPut("recover-pass")]
        public async Task<IActionResult> RecoverPasswordAsync([FromBody] RecoverPasswordDTO recoverPassDTO)
        {
            // Validation (cài package FluentValidation nha N)
            var validationResult = await recoverPassDTO.ValidateAsync();
            // Is not valid
            if (validationResult is not null)
            {
                // Response BadRequest[400]
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Something went wrong",
                    // Map key value, each property may has many error messages
                    Errors = validationResult.Errors
                });
            }


            // Process recover password...
            // Get user by email
            var user = _db.Users.SingleOrDefault(x => x.Email.Equals(recoverPassDTO.Email));
            if (user is null) // Not found user
            {
                // Response NotFound[404]
                return NotFound(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = $"Not found any user with email {recoverPassDTO.Email}",
                });
            }

            var encoder = new AesEncryption("Huhuhutoikhongmuoncodenua");
            string decrypted = encoder.Decrypt(recoverPassDTO.Token);

            try
            {
                JObject jsonObject = JObject.Parse(decrypted);

                var email = jsonObject["email"]?.ToString();
                var code = jsonObject["code"]?.ToString();

                if (jsonObject["expired_time"] is JToken expiredTimeToken && expiredTimeToken.Type != JTokenType.Null)
                {
                    DateTime expiredTime;
                    try
                    {
                        expiredTime = expiredTimeToken.ToObject<DateTime>();
                    }
                    catch (Exception)
                    {
                        return BadRequest(new { Message = "Invalid expiration time format." });
                    }
                    if (email == null || code == null)
                    {
                        return BadRequest(new { Message = "Missing email or code in the token." });
                    }
                    if (recoverPassDTO.Email != email)
                    {
                        return Forbid();
                    }
                    // Get current password 
                    var currentPassword = user.Password;
                    // Change password
                    user.Password = recoverPassDTO.Password;

                    // Publish to message brocker to synchronize data
                    await _publishEndpoint.Publish(new UserChangePassword
                    {
                        Username = user.Username,
                        CurrentPassword = currentPassword,
                        NewPassword = user.Password
                    });

                    // Save db
                    var result = await _db.SaveChangesAsync() > 0;

                    // Process response...
                    return result
                        ? Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Recover password successfully" })
                        : StatusCode(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    return BadRequest(new { Message = "Expiration time is missing or null." });
                }
            }
            catch
            {
                return BadRequest(new { Message = "Error" });
            }
        }
    }
}
