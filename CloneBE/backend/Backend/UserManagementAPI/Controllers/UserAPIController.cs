using AutoMapper;
using Azure;
using ClosedXML.Excel;
using Contracts.UserManagement;
using Entities.Context;
using Entities.Models;
using ExcelDataReader;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nest;
using System.Data;
using System.Globalization;
using System.Text;
using UserManagementAPI.Models.DTO;
using UserManagementAPI.Models.DTO.ValidationDTO;
using UserManagementAPI.Utils;

namespace UserManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize]
    public class UserAPIController : ControllerBase
    {
        private readonly FamsContext _db;
        private readonly UserResponse _response;
        private readonly IMapper _mapper;
        private readonly IElasticClient _client;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserAPIController(IPublishEndpoint publishEndpoint, FamsContext db, IMapper mapper, IElasticClient client)
        {
            _publishEndpoint = publishEndpoint;
            _db = db;
            _response = new UserResponse();
            _mapper = mapper;
            _client = client;

        }

        private string GetPermsName(string id)
        {
            var permission = _db.UserPermissions.FirstOrDefault(p => p.UserPermissionId == id);
            if (permission == null)
            {
                return "Fail";
            }
            else
            {
                return permission.Name;
            }

        }

        [HttpGet("user-info")]
        public IActionResult UserInfo(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username is required.");
            }

            try
            {
                var existingUser = _db.Users.FirstOrDefault(u => u.Username == username);
                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }

                // Assuming a 1:1 relationship between User and RolePermission for simplicity
                var rolePermission = _db.RolePermissions.FirstOrDefault(rp => rp.RoleId == existingUser.RoleId);
                if (rolePermission == null)
                {
                    return BadRequest("Permissions not found for the user's role.");
                }

                var permissions = new
                {
                    Syllabus = GetPermsName(rolePermission.Syllabus),
                    TrainingProgram = GetPermsName(rolePermission.TrainingProgram),
                    Class = GetPermsName(rolePermission.Class),
                    LearningMaterial = GetPermsName(rolePermission.LearningMaterial),
                    UserManagement = GetPermsName(rolePermission.UserManagement)
                };

                var userInfo = new
                {
                    FullName = existingUser.FullName,
                    Address = existingUser.Address,
                    Email = existingUser.Email,
                    Phone = existingUser.Phone,
                    Gender = existingUser.Gender,
                    Dob = existingUser.Dob,
                    Status = existingUser.Status,
                    Permissions = permissions
                };

                var response = new ResponseDTO
                {
                    Result = userInfo,
                    StatusCode = 200,
                    IsSuccess = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("pageination/{sortBy}")]
        public IActionResult Pagination(int page = 1, int pageSize = 10, string sortBy = "id", string sortOrder = "asc")
        {
            try
            {
                IQueryable<User> query = _db.Users;

                // Sorting
                switch (sortBy.ToLower())
                {
                    case "id":
                        query = sortOrder.ToLower() == "asc" ? query.OrderBy(u => u.Id) : query.OrderByDescending(u => u.Id);
                        break;
                    case "name":
                        query = sortOrder.ToLower() == "asc" ? query.OrderBy(u => u.FullName) : query.OrderByDescending(u => u.FullName);
                        break;
                    case "email":
                        query = sortOrder.ToLower() == "asc" ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                        break;
                    case "dob":
                        query = sortOrder.ToLower() == "asc" ? query.OrderBy(u => u.Dob) : query.OrderByDescending(u => u.Dob);
                        break;
                    case "gender":
                        query = sortOrder.ToLower() == "asc" ? query.OrderBy(u => u.Gender) : query.OrderByDescending(u => u.Gender);
                        break;
                    case "type":
                        query = sortOrder.ToLower() == "asc" ? query.OrderBy(u => u.Role.RoleName) : query.OrderByDescending(u => u.Role.RoleName);
                        break;
                    default:
                        query = query.OrderBy(u => u.Id);
                        break;
                }
                var totalCount = query
                  .Where(user => user.Role != null && user.Role.RoleName != "Super Admin")
                  .Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var usersPerPage = query
                  .Include(user => user.Role)
                  .Where(user => user.Role != null && (user.Role.RoleName == "Admin" || user.Role.RoleName == "Trainer"))
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .Select(user => new UserDTO
                  {
                      UserId = user.UserId,
                      FullName = user.FullName,
                      Email = user.Email,
                      Phone = user.Phone,
                      Dob = user.Dob,
                      Gender = user.Gender,
                      Status = user.Status,
                      RoleName = user.Role != null ? user.Role.RoleName : null
                  })
                  .ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Users = usersPerPage
                };
                var response = new ResponseDTO();
                response.Result = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpPost("create-user")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDTO userDto)
        {
            var response = new ResponseDTO("Failed to create user.", 500, false);

            try
            {
                User obj = _mapper.Map<User>(userDto);

                if (string.IsNullOrEmpty(userDto.FullName))
                {
                    response.Message = "Fullname is required";
                    return BadRequest(response);
                }

                if (_db.Users.Any(u => u.Username == userDto.Username && u.Id != userDto.Id))
                {
                    response.Message = "Username already exists";
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(userDto.Email))
                {
                    response.Message = "Email is required";
                    return BadRequest(response);
                }

                if (!MyUtils.ValidateGmail(userDto.Email))
                {
                    response.Message = "Email is in wrong format (Format: example@gmail.com)";
                    return BadRequest(response);
                }

                if (_db.Users.Any(u => u.Email == userDto.Email && u.Id != userDto.Id))
                {
                    response.Message = "Email already exists";
                    return BadRequest(response);
                }

                if (!MyUtils.ValidatePassword(userDto.Password.Trim()))
                {
                    response.Message = "Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one number, and one special character.";
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(userDto.Phone))
                {
                    response.Message = "Phone number is required";
                    return BadRequest(response);
                }

                if (_db.Users.Any(u => u.Phone == userDto.Phone && u.Id != userDto.Id))
                {
                    response.Message = "Phone number already exists";
                    return BadRequest(response);
                }

                if (userDto.Phone.Trim().Length != 10)
                {
                    response.Message = "Phone number must be 10 digits";
                    return BadRequest(response);
                }

                if (userDto.Dob == DateTime.MinValue)
                {
                    response.Message = "Date of birth is required";
                    return BadRequest(response);
                }

                if (MyUtils.GreaterThan18(userDto.Dob))
                {
                    response.Message = "Age must be 18 or above";
                    return BadRequest(response);
                }

                if (userDto.Dob >= DateTime.Today)
                {
                    response.Message = "Date of birth must not be in the future";
                    return BadRequest(response);
                }
                _db.Users.Add(obj);

                try
                {
                    response.Message = "User created successfully";
                    response.StatusCode = 200;
                    response.IsSuccess = true;
                    await _db.SaveChangesAsync();

                    // Process check validation here... (if any)

                    // Publish message to message broker for UserManagementAPI add new user data
                    // Map model to intermidate model from Contracts
                    var userCreated = _mapper.Map<UserCreated>(obj);

                    // Publish to Message Broker (RabbitMQ Exchange)
                    await _publishEndpoint.Publish(userCreated);
                    await _client.IndexDocumentAsync(obj);

                }
                catch
                {
                    response.Message = "Failed to add user.";
                    return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("check-mail")]
        public IActionResult CheckEmailExists([FromBody] EmailValidationDTO emailDTO)
        {
            string email = emailDTO.Email;

            if (!MyUtils.ValidateGmail(email))
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Wrong format" });
            }

            var exists = _db.Users.Any(u => u.Email == email);
            if (exists)
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Email already exists" });
            }
            else
            {
                return Ok(new ResponseDTO { IsSuccess = true, Message = "Email available" });
            }
        }

        [HttpPost("check-username")]
        public IActionResult CheckUsernameExists([FromBody] UNameValidationDTO unameDTO)
        {
            string uname = unameDTO.Username;

            var exists = _db.Users.Any(u => u.Username == uname);
            if (exists)
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Username already exists" });
            }
            else
            {
                return Ok(new ResponseDTO { IsSuccess = true, Message = "Username available" });
            }
        }

        [HttpPost("check-phone")]
        public IActionResult CheckPhoneExists([FromBody] PhoneValidationDTO phoneDTO)
        {
            string phone = phoneDTO.Phone;

            if (phone.Length != 10) return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Phone must be a 10-digit number" });

            var exists = _db.Users.Any(u => u.Phone == phone);
            if (exists)
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Phone number already exists" });
            }
            else
            {
                return Ok(new ResponseDTO { IsSuccess = true, Message = "Phone number available" });
            }
        }

        [HttpPut("update-user")]
        public async Task<ResponseDTO> UpdateUser([FromBody] UpdateUserDTO userDto)
        {
            try
            {
                var existingUser = _db.Users.ToList().FirstOrDefault(u => u.UserId == userDto.UserId);
                if (existingUser == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "User does not exist."
                    };
                }

                //validate thông tin user
                // Validate fullname
                if (string.IsNullOrEmpty(userDto.FullName))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Fullname is required"
                    };
                }

                // Validate Phone

                if (string.IsNullOrEmpty(userDto.Phone))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Phone number is required"
                    };
                }

                if (_db.Users.Any(u => u.Phone == userDto.Phone && !(u.UserId == userDto.UserId)))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Phone number already exists"
                    };
                }

                if (userDto.Phone.Length != 10)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Phone number must be 10-digit number"
                    };
                }

                // Validate dob 
                if (userDto.Dob == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Date of birth is required"
                    };
                }

                if (userDto.Dob >= DateTime.Today)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Date of birth must not be in the future"
                    };
                }

                if (!MyUtils.IsOver18((DateTime)userDto.Dob))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "User must be at least 18 years old"
                    };
                }
                // Validate roleid
                if (userDto.RoleId.IsNullOrEmpty())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "RoleId is required"
                    };
                }

                var existingRole = _db.Roles.FirstOrDefault(r => r.RoleId == userDto.RoleId);
                if (existingRole == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The RoleId does not exist"
                    };
                }
                string roleName = existingRole.RoleName!;
                existingUser.FullName = userDto.FullName;
                existingUser.Phone = userDto.Phone;
                existingUser.Dob = (DateTime)userDto.Dob;
                existingUser.Gender = userDto.Gender;
                existingUser.Status = userDto.Status;
                existingUser.ModifiedDate = DateTime.Today;
                existingUser.RoleId = userDto.RoleId;

                // Publish to message brocker to synchronize data with Identity Server
                await _publishEndpoint.Publish(_mapper.Map<UserUpdated>(existingUser));


                var userToUpdate = new
                {
                    userId = existingUser.UserId.ToString(),
                    fullName = existingUser.FullName,
                    email = existingUser.Email,
                    dob = existingUser.Dob.ToString("yyyy-MM-ddTHH:mm:ss"),
                    address = existingUser.Address,
                    gender = existingUser.Gender,
                    phone = existingUser.Phone,
                    roleId = existingUser.RoleId,
                    roleName = roleName,
                    status = existingUser.Status,
                };

                var searchResponse = _client.Search<User>(s => s
                  .Query(q => q
                    .Match(m => m
                      .Field(f => f.UserId)
                      .Query(existingUser.UserId))));
                if (searchResponse.Hits.Any())
                {
                    await _db.SaveChangesAsync();
                    var documentId = searchResponse.Hits.First().Id;
                    var updateResponse = _client.UpdateAsync<User,
                      object>(documentId, u => u
                        .Doc(userToUpdate)
                      );


                    return new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Updated successfully."
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "An error occurred while update user."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "An error occurred while update user: " + ex.Message
                };
            }
        }

        [HttpPut("update-info")]
        public async Task<ResponseDTO> UpdateInfo([FromBody] UpdateInfoDTO userDto)
        {
            try
            {
                var existingUser = _db.Users.ToList().FirstOrDefault(u => u.Username == userDto.Username);
                if (existingUser == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "User does not exist."
                    };
                }

                //validate thông tin user
                // Validate fullname
                if (string.IsNullOrEmpty(userDto.FullName))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Fullname is required"
                    };
                }

                // Validate Phone

                if (string.IsNullOrEmpty(userDto.Phone))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Phone number is required"
                    };
                }

                if (_db.Users.Any(u => u.Phone == userDto.Phone && !(u.Username == userDto.Username)))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Phone number already exists"
                    };
                }

                if (userDto.Phone.Length != 10)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Phone number must be 10-digit number"
                    };
                }

                if (string.IsNullOrEmpty(userDto.Email))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Email cannot be null."
                    };
                }

                if (!MyUtils.ValidateGmail(userDto.Email))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Email is in wrong format (Format: example@gmail.com)"
                    };
                }

                if (_db.Users.Any(u => u.Email == userDto.Email && u.Username != userDto.Username))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Email already exists"
                    };
                }

                if (userDto.Address.Length == 0)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Address cannot be null"
                    };
                }

                // Validate dob 
                if (userDto.Dob == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Date of birth is required"
                    };
                }

                if (userDto.Dob >= DateTime.Today)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Date of birth must not be in the future"
                    };
                }

                if (!MyUtils.IsOver18((DateTime)userDto.Dob))
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "User must be at least 18 years old"
                    };
                }
               
                existingUser.FullName = userDto.FullName;
                existingUser.Phone = userDto.Phone;
                existingUser.Email = userDto.Email;
                existingUser.Address = userDto.Address;
                existingUser.Dob = (DateTime)userDto.Dob;
                existingUser.Gender = userDto.Gender;
                existingUser.ModifiedDate = DateTime.Today;

                // Publish to message brocker to synchronize data with Identity Server
                await _publishEndpoint.Publish(_mapper.Map<UserChangeInfo>(existingUser));

                var existingRole = _db.Roles.FirstOrDefault(r => r.RoleId == existingUser.RoleId);
                if (existingRole == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The RoleId does not exist"
                    };
                }
                string roleName = existingRole.RoleName!;


                var userToUpdate = new
                {
                    userId = existingUser.UserId.ToString(),
                    fullName = existingUser.FullName,
                    email = existingUser.Email,
                    dob = existingUser.Dob.ToString("yyyy-MM-ddTHH:mm:ss"),
                    address = existingUser.Address,
                    gender = existingUser.Gender,
                    phone = existingUser.Phone,
                    roleId = existingUser.RoleId,
                    roleName = roleName,
                    status = existingUser.Status,
                };

                var searchResponse = _client.Search<User>(s => s
                  .Query(q => q
                    .Match(m => m
                      .Field(f => f.UserId)
                      .Query(existingUser.UserId))));
                if (searchResponse.Hits.Any())
                {
                    await _db.SaveChangesAsync();
                    var documentId = searchResponse.Hits.First().Id;
                    var updateResponse = _client.UpdateAsync<User,
                      object>(documentId, u => u
                        .Doc(userToUpdate)
                      );


                    return new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Updated successfully."
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "An error occurred while update user."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "An error occurred while update user: " + ex.Message
                };
            }
        }

        [HttpPut("change-role")]
        public async Task<ResponseDTO> ChangeRole([FromBody] ChangeUserRoleDTO requestData)
        {
            try
            {
                // tìm người dùng trong CSDL dựa trên Id
                var existingUser = _db.Users.FirstOrDefault(u => u.UserId == requestData.userId);

                // trả về lỗi nếu không tìm ra 
                if (existingUser == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The User does not exist!!!!"
                    };
                }
                // Validate roleid
                if (requestData.roleId.IsNullOrEmpty())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The RoleId is required"
                    };
                }

                var existingRole = _db.Roles.FirstOrDefault(r => r.RoleId == requestData.roleId);

                if (existingRole == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The RoleId does not exist"
                    };
                }
                string roleName = existingRole.RoleName!;
                existingUser.RoleId = requestData.roleId;

                // Publish to message brocker to synchronize data with Identity Server
                await _publishEndpoint.Publish(new UserChangeRole
                {
                    Username = existingUser.Username,
                    RoleId = requestData.roleId
                });

                var userToUpdate = new
                {
                    userId = existingUser.UserId.ToString(),
                    fullName = existingUser.FullName,
                    email = existingUser.Email,
                    dob = existingUser.Dob.ToString("yyyy-MM-ddTHH:mm:ss"),
                    address = existingUser.Address,
                    gender = existingUser.Gender,
                    phone = existingUser.Phone,
                    roleId = requestData.roleId,
                    roleName = roleName,
                    status = existingUser.Status,
                };

                var searchResponse = _client.Search<User>(s => s
                  .Query(q => q
                    .Match(m => m
                      .Field(f => f.UserId)
                      .Query(requestData.userId))));

                if (searchResponse.Hits.Any())
                {
                    var documentId = searchResponse.Hits.First().Id;
                    var updateResponse = _client.UpdateAsync<User, object>(documentId, u => u.Doc(userToUpdate));
                    _db.SaveChanges();

                    return new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Change role successfully"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "An error occurred while changing role."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "An error occurred while changing role: " + ex.Message
                };
            }
        }

        [HttpPut("change-status")]
        public async Task<ResponseDTO> ChangeStatus([FromBody] ChangeStatusDTO changeStatusDTO)
        {
            try
            {
                var existingUser = _db.Users.FirstOrDefault(u => u.UserId == changeStatusDTO.userId);
                if (existingUser == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "User does not exist"
                    };
                }
                existingUser.Status = changeStatusDTO.status;

                // Publish to message brocker to synchronize data with Identity Server
                await _publishEndpoint.Publish(new UserChangeStatus
                {
                    Username = existingUser.Username,
                    Status = existingUser.Status
                });

                var existingRole = _db.Roles.FirstOrDefault(r => r.RoleId == existingUser.RoleId);

                if (existingRole == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "The RoleId does not exist"
                    };
                }
                string roleName = existingRole.RoleName!;

                var userToUpdate = new
                {
                    userId = existingUser.UserId.ToString(),
                    fullName = existingUser.FullName,
                    email = existingUser.Email,
                    dob = existingUser.Dob.ToString("yyyy-MM-ddTHH:mm:ss"),
                    address = existingUser.Address,
                    gender = existingUser.Gender,
                    phone = existingUser.Phone,
                    roleId = existingUser.RoleId.ToString(),
                    roleName = roleName,
                    status = changeStatusDTO.status,
                };

                var searchResponse = _client.Search<User>(s => s
                  .Query(q => q
                    .Match(m => m
                      .Field(f => f.UserId)
                      .Query(changeStatusDTO.userId))));

                if (searchResponse.Hits.Any())
                {
                    var documentId = searchResponse.Hits.First().Id;
                    var updateResponse = _client.UpdateAsync<User,
                      object>(documentId, u => u
                        .Doc(userToUpdate)
                      );

                    _db.SaveChanges();

                    return new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Change status successfully"
                    };
                }
                else
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "An error occurred while changing status."
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "An error occurred while changing status: " + ex.Message
                };
            }
        }

        [HttpGet("filter-user/{sortBy}")]
        public IActionResult Filter(int page = 1, int pageSize = 10, string sortBy = "id", string sortOrder = "asc", DateTime? dob = null, bool isAdmin = true, bool isTrainer = true, bool isActive = true, bool isInactive = true, bool isMale = true, bool isFemale = true)
        {
            try
            {
                IQueryable<User> query = _db.Users;
                switch (sortBy.ToLower())
                {
                    case "id":
                        query = sortOrder.ToLower() == "asc" ?
                          query.OrderBy(u => u.Id) :
                          query.OrderByDescending(u => u.Id);
                        break;
                    case "name":
                        query = sortOrder.ToLower() == "asc" ?
                          query.OrderBy(u => u.FullName) :
                          query.OrderByDescending(u => u.FullName);
                        break;
                    case "email":
                        query = sortOrder.ToLower() == "asc" ?
                          query.OrderBy(u => u.Email) :
                          query.OrderByDescending(u => u.Email);
                        break;
                    case "dob":
                        query = sortOrder.ToLower() == "asc" ?
                          query.OrderBy(u => u.Dob) :
                          query.OrderByDescending(u => u.Dob);
                        break;
                    case "gender":
                        query = sortOrder.ToLower() == "asc" ?
                          query.OrderBy(u => u.Gender) :
                          query.OrderByDescending(u => u.Gender);
                        break;
                    case "type":
                        query = sortOrder.ToLower() == "asc" ?
                          query.OrderBy(u => u.Role.RoleName) :
                          query.OrderByDescending(u => u.Role.RoleName);
                        break;
                    default:
                        query = query.OrderBy(u => u.Id);
                        break;
                }
                query = query.Where(user => user.Role != null && user.Role.RoleName != "Super Admin");
                if (dob != null)
                {
                    query = query.Where(u => u.Dob.Equals(dob));
                }

                if (isAdmin != isTrainer)
                {
                    if (isAdmin)
                    {
                        query = query.Where(u => u.Role.RoleName == "Admin");
                    }
                    if (isTrainer)
                    {
                        query = query.Where(u => u.Role.RoleName == "Trainer");
                    }
                }
                if (isActive != isInactive)
                {
                    if (isActive)
                    {
                        query = query.Where(u => u.Status == true);
                    }
                    if (isInactive)
                    {
                        query = query.Where(u => u.Status == false);
                    }
                }
                if (isMale != isFemale)
                {
                    if (isMale)
                    {
                        query = query.Where(u => u.Gender.ToUpper() == "MALE");
                    }
                    if (isFemale)
                    {
                        query = query.Where(u => u.Gender.ToUpper() == "FEMALE");
                    }
                }

                var totalCount = query.Count();

                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var usersPerPage = query
                  .Include(user => user.Role)
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .Select(user => new UserDTO
                  {
                      UserId = user.UserId,
                      FullName = user.FullName,
                      Email = user.Email,
                      Phone = user.Phone,
                      Dob = user.Dob,
                      Gender = user.Gender,
                      Status = user.Status,
                      RoleId = user.RoleId,
                      RoleName = user.Role != null ? user.Role.RoleName : null
                  })
                  .ToList();

                var result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Users = usersPerPage
                };

                var response = new ResponseDTO();
                response.Result = result;
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }


        [HttpPost("sync-to-elasticsearch")]
        public async Task<IActionResult> SyncToElasticsearch()
        {
            try
            {
                var users = await _db.Users
                    .Include(u => u.Role)
                    .ToListAsync();

                foreach (var user in users)
                {
                    var userElasticDTO = new UserElasticDTO
                    {
                        UserId = user.UserId.ToString(),
                        FullName = user.FullName,
                        Email = user.Email,
                        Dob = user.Dob.ToString("yyyy-MM-ddTHH:mm:ss"),
                        Address = user.Address,
                        Gender = user.Gender,
                        Phone = user.Phone,
                        RoleId = user.RoleId.ToString(),
                        RoleName = user.Role.RoleName.ToString(),
                        Status = user.Status
                    };
                    var response = await _client.IndexAsync(userElasticDTO, i => i
                        .Index("user")
                        .Id(user.Id));

                    if (!response.IsValid)
                    {
                        Console.WriteLine($"Failed to index user {userElasticDTO.UserId}: {response.ServerError.Error.Reason}");
                    }
                }
                return Ok("Synced to Elasticsearch successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("import-user")]
        public async Task<IActionResult> ImportUser(IFormFile file)
        {
            var response = new ResponseDTO
            {
                Message = "",
                IsSuccess = false,
                StatusCode = 500
            };
            try
            {

                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();


                if (file != null && file.Length > 0)
                {
                    Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    Stream stream = file.OpenReadStream();
                    IExcelDataReader? reader = null;
                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        response.Message = "this file fotmat is not suppported";
                        return Ok(response);
                    }

                    try
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = reader.AsDataSet().Tables[0];
                        result = dataTable.AsEnumerable()
                            .Select(row => dataTable.Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => row[col]))
                            .ToList();
                    }
                    catch (Exception ex)
                    {
                        response.Message = "An error occurred while upload: " + ex.Message;
                        return Ok(response);
                    }
                    reader.Close();
                    reader.Dispose();
                }

                List<UserDTO> userList = new List<UserDTO>();
                try
                {
                    int count = 0;
                    foreach (Dictionary<string, object> item in result)
                    {
                        if (count > 0)
                        {
                            UserDTO userDto = new UserDTO();
                            userDto.FullName = item["Column0"].ToString().Trim();
                            userDto.Email = item["Column1"].ToString().Trim();
                            string dateString = item["Column2"].ToString();
                            string[] formats = { "MM-dd-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "d/M/yyyy" };
                            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                            {
                                userDto.Dob = dob;
                                string formattedDate = dob.ToString("MM-dd-yyyy");
                            }
                            else
                            {
                                response.Message += $"User No.{count} : Date of birth is required and must be in correct format (MM/dd/yyyy, etc.), ";
                                continue;
                            }
                            userDto.Address = item["Column3"].ToString().Trim();
                            userDto.Gender = item["Column4"].ToString().ToUpper().Trim();
                            userDto.Phone = item["Column5"].ToString().Trim();
                            userDto.Username = item["Column6"].ToString().Trim();
                            userDto.Password = item["Column7"].ToString().Trim();
                            userDto.Status = MyUtils.ValidStatus(item["Column8"].ToString());
                            userDto.RoleName = item["Column9"].ToString().Trim();
                            // role id
                            var Role = _db.Roles.FirstOrDefault(r => r.RoleName == userDto.RoleName);
                            if (Role == null)
                            {
                                response.Message = "User No." + count + " : " + "Role does not exist";
                                return Ok(response);
                            }
                            userDto.RoleId = Role.RoleId;
                            userList.Add(userDto);
                        }
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while add user to UserList: " + ex.Message;
                    return Ok(response);
                }
                try
                {
                    int count = 0;
                    foreach (var userDto in userList)
                    {
                        //Anh Vinh có thấy thì là thằng Đào code đó anh
                        count++;
                        // Verify trong file excel
                        if (userList.Select(u => u.Email).ToList().Count(e => e == userDto.Email) > 1)
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Email is duplicate in excel file" + ", ";
                        }

                        if (userList.Select(u => u.Phone).ToList().Count(p => p == userDto.Phone) > 1)
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Phone is duplicate in excel file" + ", ";
                        }

                        if (userList.Select(u => u.Username).ToList().Count(u => u == userDto.Username) > 1)
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Username is dulicate in excel file" + ", ";
                        }

                        // Verify trong db
                        if (string.IsNullOrEmpty(userDto.FullName))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Fullname is required" + ", ";
                        }

                        if (!MyUtils.IsOnlyCharacters(userDto.FullName))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Fullname must only have character" + ", ";
                        }

                        if (_db.Users.Any(u => u.Username == userDto.Username))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Username already exists" + ", ";
                        }

                        if (userDto.Username.IsNullOrEmpty())
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Username is required" + ", ";
                        }

                        if (string.IsNullOrEmpty(userDto.Email))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Email is required" + ", ";
                        }

                        if (!MyUtils.ValidateGmail(userDto.Email))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Email is in wrong format (Format: example@gmail.com)" + ", ";
                        }

                        if (_db.Users.Any(u => u.Email == userDto.Email))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Email already exists" + ", ";
                        }

                        if (userDto.Password.IsNullOrEmpty())
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Password is required" + ", ";
                        }
                        if (!MyUtils.ValidatePassword(userDto.Password.Trim()))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one number, and one special character." + ", ";
                        }

                        if (string.IsNullOrEmpty(userDto.Phone))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Phone number is required" + ", ";
                        }

                        if (_db.Users.Any(u => u.Phone == userDto.Phone))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Phone number already exists" + ", ";
                        }

                        if (userDto.Phone.Trim().Length != 10)
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Phone number must be 10 digits" + ", ";
                        }

                        if (!MyUtils.ValidatePhone(userDto.Phone.Trim()))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Not a number phone" + ", ";
                        }

                        if (userDto.Dob == null)
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Date of birth is required (format: MM/DD/YY)" + ", ";
                        }

                        if (userDto.Dob >= DateTime.Today)
                        {
                            response.Message = response.Message + "User No." + count + " : " + "Date of birth must not be in the future" + ", ";
                        }

                        if (!MyUtils.IsOver18((DateTime)userDto.Dob))
                        {
                            response.Message = response.Message + "User No." + count + " : " + "User must be at least 18 years old" + ", ";
                        }
                    }
                    if (response.Message != "")
                    {
                        return BadRequest(response);
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while verify : " + ex.Message;
                    return Ok(response);
                }
                try
                {
                    foreach (UserDTO userDto in userList)
                    {
                        User obj = _mapper.Map<User>(userDto);
                        _db.Users.Add(obj);
                        await _db.SaveChangesAsync();
                        var userCreated = _mapper.Map<UserCreated>(obj);
                        // Publish to message brocker to synchronize data with Identity Server
                        await _publishEndpoint.Publish(userCreated);
                        //Sync to Elastic
                        await _client.IndexDocumentAsync(obj);
                    }
                }
                catch (Exception ex)
                {
                    response.Message = "An error occurred while saving to db : " + ex.Message;
                    return Ok(response);
                }
                response.Message = "import success";
                response.StatusCode = 200;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while import : " + ex.Message;
                return Ok(response);
            }
        }

        [HttpGet("export-user")]
        public async Task<ActionResult> ExportUser(DateTime? dob = null, bool isAdmin = true, bool isTrainer = true, bool isActive = true, bool isInactive = true, bool isMale = true, bool isFemale = true)
        {
            var response = new ResponseDTO
            {
                Message = "",
                IsSuccess = false,
                StatusCode = 500
            };
            try
            {
                var _empdata = GetEmpdate(dob, isAdmin, isTrainer, isActive, isInactive, isMale, isFemale);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.AddWorksheet(_empdata, "ExportUserList");
                    using (MemoryStream ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportUserList.xlsx");
                    }

                    response = new ResponseDTO
                    {
                        Message = "Export success",
                        IsSuccess = true,
                        StatusCode = 200,
                    };
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred while exporting users: " + ex.Message;
                return BadRequest(response);
            }
        }

        [NonAction]
        private DataTable GetEmpdate(DateTime? dob = null, bool isAdmin = true, bool isTrainer = true, bool isActive = true, bool isInactive = true, bool isMale = true, bool isFemale = true)
        {
            DataTable dt = new DataTable();
            dt.TableName = "ExportUserList";
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Dob", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Role", typeof(string));


            IQueryable<User> query = _db.Users;

            if (dob != null)
            {
                query = query.Where(u => u.Dob.Equals(dob));
            }

            if (isAdmin != isTrainer)
            {
                if (isAdmin)
                {
                    query = query.Where(u => u.Role.RoleName == "Admin");
                }
                if (isTrainer)
                {
                    query = query.Where(u => u.Role.RoleName == "Trainer");
                }
            }
            if (isActive != isInactive)
            {
                if (isActive)
                {
                    query = query.Where(u => u.Status == true);
                }
                if (isInactive)
                {
                    query = query.Where(u => u.Status == false);
                }
            }
            if (isMale != isFemale)
            {
                if (isMale)
                {
                    query = query.Where(u => u.Gender.ToUpper() == "MALE");
                }
                if (isFemale)
                {
                    query = query.Where(u => u.Gender.ToUpper() == "FEMALE");
                }
            }

            var userList = query.ToList();

            if (userList.Count > 0)
            {
                foreach (var item in userList)
                {
                    if (GetRoleName(item.RoleId).ToUpper() != "SUPER ADMIN")
                    {
                        dt.Rows.Add(item.FullName,
                        item.Email,
                        item.Dob.ToString("MM-dd-yyyy"),
                        item.Address,
                        item.Gender,
                        item.Phone,
                        item.Username,
                        item.Status,
                        GetRoleName(item.RoleId));
                    }
                }
            }
            return dt;
        }

        private string GetRoleName(string? roleId)
        {
            if (roleId != null)
            {
                Role currentRole = _db.Roles.FirstOrDefault(x => x.RoleId == roleId);
                return currentRole.RoleName;
            }
            return null;
        }

        [NonAction]
        private DataTable GetEmpdate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "ExportUserList";
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Dob", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Role", typeof(string));
            var userList = _db.Users.ToList();
            if (userList.Count > 0)
            {
                foreach (var item in userList)
                {
                    if (GetRoleName(item.RoleId).ToUpper() != "SUPER ADMIN")
                    {
                        dt.Rows.Add(item.FullName,
                        item.Email,
                        item.Dob.ToString("MM-dd-yyyy"),
                        item.Address,
                        item.Gender,
                        item.Phone,
                        item.Username,
                        item.Status,
                        GetRoleName(item.RoleId));
                    }
                }
            }
            return dt;
        }
    }
}