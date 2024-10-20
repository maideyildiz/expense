using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ExpenseTracker.Core.Models;
using Microsoft.Extensions.Configuration;
using ExpenseTracker.Infrastructure.Abstractions.Auth;
using ExpenseTracker.Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Infrastructure.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly IUserService _userService;
    private readonly ILogger<TokenService> _logger;

    public TokenService(IConfiguration config, IUserService userService, ILogger<TokenService> logger)
    {
        this._config = config;
        this._secretKey = _config["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey is not set");
        this._issuer = _config["JwtSettings:Issuer"] ?? throw new ArgumentNullException("JwtSettings:Issuer is not set");
        this._audience = _config["JwtSettings:Audience"] ?? "defaultAudience"; // Varsayılan bir değer atanabilir
        this._userService = userService;
        this._logger = logger;
    }


    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            //new Claim(ClaimTypes.Role, user.Role) // Add user roles if necessary
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1), // This could come from configuration
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch (SecurityTokenExpiredException ex)
        {
            _logger.LogError("Token has expired: {message}", ex.Message);
            throw new SecurityTokenException("Token has expired.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Token validation failed: {message}", ex.Message);
            throw new SecurityTokenException("Invalid token.", ex);
        }
    }

    public async Task<string> RefreshToken(string expiredToken)
    {
        var principal = ValidateToken(expiredToken);
        if (principal == null)
        {
            throw new SecurityTokenException("Invalid token");
        }

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null || !Guid.TryParse(userId, out Guid userIdGuid))
        {
            throw new SecurityTokenException("Invalid token claims");
        }

        var user = await _userService.GetByIdAsync(userIdGuid);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid token claims");
        }

        return GenerateToken(user);
    }

    public void RevokeToken(string token)
    {
        // Implementation for revoking tokens
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? GetClaimsFromToken(string token)
    {
        return ValidateToken(token);
    }
}
