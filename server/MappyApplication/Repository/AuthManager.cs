using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MappyApplication.Contract;
using MappyApplication.Data;
using MappyApplication.Exceptions;
using MappyApplication.Models.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MappyApplication.Repository;

public class AuthManager : IAuthManager
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IConfiguration _configuration;
    private readonly MappyDBContext _dbContext;
    private readonly ILogger<AuthManager> _logger;
    private readonly IMapper _mapper;
    private readonly ITokenRepository _tokenRepository;
    private readonly UserManager<User> _userManager;

    public AuthManager(IHttpContextAccessor accessor, IConfiguration configuration, ILogger<AuthManager> logger,
        IMapper mapper, ITokenRepository tokenRepository, UserManager<User> userManager, MappyDBContext dbContext)
    {
        _accessor = accessor;
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
        _tokenRepository = tokenRepository;
        _userManager = userManager;
        _dbContext = dbContext;
    }

    private HttpContext _context => _accessor.HttpContext;


    public async Task<AuthResponse> Register(AuthRegister authRegister)
    {
        try
        {
            var isExist = await _userManager.FindByEmailAsync(authRegister.Email);
            if (isExist != null) throw new AppErrorResponse("Email address already exists");
            var user = _mapper.Map<User>(authRegister);
            user.UserName = authRegister.Email;
            var result = await _userManager.CreateAsync(user, authRegister.Password);
            if (result.Succeeded) await _userManager.AddToRoleAsync(user, "user");
            if (result.Errors.Any()) throw new AppErrorResponse(result.Errors.SingleOrDefault().Description);
            var deviceId = GetOrCreateDeviceId();

            return await GetResponse(user, deviceId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw new AppErrorResponse(e.Message);
        }
    }

    public async Task<AuthResponse> Login(AuthLogin authLogin)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(authLogin.Email);
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, authLogin.Password);
            if (user == null || !isPasswordValid) throw new AppErrorResponse("Incorrect username or password");
            var deviceId = GetOrCreateDeviceId();
            return await GetResponse(user, deviceId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw new AppErrorResponse(e.Message);
        }
    }

    public async Task<AuthResponse> Refresh()
    {
        try
        {
            var deviceId = GetDeviceId();
            var jwt = _context.Request.Cookies["jwt"];
            var token = _tokenRepository.FindByRefreshToken(jwt);
            var user = await _userManager.FindByIdAsync(token.UserId);

            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(jwt) || token == null || IsJwtExpired(jwt) ||
                user == null || token.Expired || token.Revoked)
                throw new AppErrorResponse("you are not logged in");
            return await GetResponse(user, deviceId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw new AppErrorResponse(e.Message);
        }
    }

    public async Task Logout()
    {
        var jwt = _context.Request.Cookies["jwt"];
        var deviceId = GetDeviceId();
        var token = _tokenRepository.FindByRefreshToken(jwt);
        if (token == null || deviceId == null || jwt == null || token.Expired || token.Revoked || IsJwtExpired(jwt))
            throw new AppErrorResponse("you are not logged in");

        await _tokenRepository.UpdateAllValidTokenByUserIdAndDeviceId(token.UserId, deviceId);
        _context.Response.Cookies.Delete("jwt");
    }

    public async Task<UserDto> GetUser()
    {
        try
        {
            var userId = _context?.User?.Claims?.SingleOrDefault(u => u.Type == "uid")?.Value;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new AppErrorResponse("No user found");

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw new AppErrorResponse(e.Message);
        }
    }

    private async Task<AuthResponse> GetResponse(User user, string deviceId)
    {
        await _tokenRepository.UpdateAllValidTokenByUserIdAndDeviceId(user.Id, deviceId);
        var accessToken = await GenerateAccessToken(user);
        var refreshToken = await GenerateRefreshToken(user);
        SetCookie("jwt", refreshToken, 7);
        await SaveToken(user, deviceId, accessToken, refreshToken);


        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = _mapper.Map<UserDto>(user)
        };
    }

    private async Task SaveToken(User user, string deviceId, string accessToken, string refreshToken)
    {
        var token = new Token
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserId = user.Id,
            DeviceId = deviceId
        };
        await _tokenRepository.AddAsync(token);
    }


    public string GetOrCreateDeviceId()
    {
        var deviceId = GetDeviceId();
        if (deviceId != null) return deviceId;

        var newDeviceId = Guid.NewGuid().ToString();
        SetCookie("deviceId", newDeviceId, 365);
        return newDeviceId;
    }

    private string GetDeviceId()
    {
        var deviceId = _context.Request.Cookies["deviceId"];

        return deviceId;
    }

    private void SetCookie(string key, string value, int maxAge)
    {
        var cookieOption = new CookieOptions();
        cookieOption.Path = "/";
        cookieOption.HttpOnly = true;
        cookieOption.Secure = true;
        cookieOption.Expires = DateTime.Now.AddDays(maxAge);
        cookieOption.SameSite = SameSiteMode.None;
        cookieOption.MaxAge = TimeSpan.FromDays(maxAge);
        _context.Response.Cookies.Append(key, value, cookieOption);
    }

    public async Task<string> GenerateAccessToken(User user)
    {
        return await BuildToken(user, 1);
    }

    public async Task<string> GenerateRefreshToken(User user)
    {
        return await BuildToken(user, 7);
    }

    public bool IsJwtExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = _configuration["Jwt:key"];
        try
        {
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty)),
                ValidIssuer = _configuration["Jwt:issuer"],
                ValidAudience = _configuration["Jwt:audience"]
            }, out var validatedToken);
            return false;
        }
        catch (SecurityTokenArgumentException e)
        {
            return true;
        }
        catch
        {
            return true;
        }
    }

    private async Task<string> BuildToken(User user, int expiresIn)
    {
        try
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.UserName),
                new("uid", user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email)
            }.Union(userClaims).Union(roleClaims);
            var token = new JwtSecurityToken(
                _configuration["Jwt:issuer"],
                _configuration["Jwt:audience"],
                expires: DateTime.UtcNow.AddDays(expiresIn),
                claims: claims,
                signingCredentials: creds,
                notBefore: DateTime.UtcNow
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw new AppErrorResponse(e.Message);
        }
    }
}