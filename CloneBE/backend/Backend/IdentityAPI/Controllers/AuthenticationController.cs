using Contracts.IdentityManagement;
using Entities.Context;
using IdentityAPI.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAPI.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    private readonly FamsContext _context;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly AppSettings _appSettings;
    private readonly IWebHostEnvironment _env;

    public AuthenticationController(UserManager<ApplicationUser> userManager,
        IPublishEndpoint publishEndpoint,
        IMapper mapper,
        FamsContext context,
        TokenValidationParameters tokenValidationParameters,
        IOptionsMonitor<AppSettings> monitor,
        IWebHostEnvironment env)
    {
        _userManager = userManager;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _context = context;
        _tokenValidationParameters = tokenValidationParameters;
        _appSettings = monitor.CurrentValue;
        _env = env;
    }

    [HttpPost(APIRoutes.Identity.Register, Name = nameof(RegisterAsync))]
    public async Task<IActionResult> RegisterAsync([FromBody] IdentityRegisterRequest reqObj)
    {
        // Validation 
        var validationResult = await reqObj.ValidateAsync();
        if (validationResult != null)
        {
            return BadRequest(new BaseResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Register failed.",
                IsSuccess = false,
                Errors = validationResult.Errors
            });
        }

        // To application user 
        var applicationUser = reqObj.ToApplicationUser(_mapper);

        // Process register 
        var result = await _userManager.CreateAsync(applicationUser, reqObj.Password);

        if (result.Succeeded)
        {
            await _userManager.AddClaimsAsync(applicationUser, new[]{
                new Claim(JwtClaimTypes.Name, applicationUser.UserName!),
                new Claim(JwtClaimTypes.Email, applicationUser.Email!)
                // new Claim(ClaimTypes.Role, applicationUser.)
            });

            // Publish message to message broker for UserManagementAPI add new user data
            // Map model to intermidate model
            var identityCreated = _mapper.Map<IdentityCreated>(applicationUser);
            // Assign password to model
            identityCreated.Password = reqObj.Password;
            // Publish to Message Broker
            await _publishEndpoint.Publish(identityCreated);

            return Ok(new BaseResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Register successfully",
                IsSuccess = true
            });
        }

        return BadRequest(result.Errors);
    }

    [HttpPost(APIRoutes.Identity.ValidateToken, Name = nameof(ValidateTokenAsync))]
    public async Task<bool> ValidateTokenAsync([FromBody] ValidateTokenRequest reqObj)
    {
        // Initiate jwt helper
        var jwtHelper = new JwtHelper(_env, _appSettings);
        // Check valid access token 
        var validationResult = await jwtHelper.ValidateAccessTokenAsync(reqObj.Token, _tokenValidationParameters);

        // Check exist identity user 
        var applicationUser = await _userManager.FindByNameAsync(reqObj.UserName);

        // Check exist user
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username.Equals(reqObj.UserName));

        // Check exist refresh token 
        var existRefreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.RefreshTokenId == reqObj.RefreshTokenId);

        // Not allowed to access web application
        if (applicationUser is null
            || user is null
            || existRefreshToken is null || !validationResult)
            return false;

        // Check if user is banned or not 
        if (!user.Status) return false;

        return true;
    }
}