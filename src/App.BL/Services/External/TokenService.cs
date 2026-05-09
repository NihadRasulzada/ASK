using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using App.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.BL.Services.External;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime ExpiresAt) CreateToken(AppUser user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        );
        var durationInMinutes = int.Parse(jwtSettings["DurationInMinutes"]!);
        var expiry = DateTime.UtcNow.AddMinutes(durationInMinutes);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName!)
    };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiry,
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = creds
        };

        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(tokenDescriptor);

        return (handler.WriteToken(securityToken), expiry);
    }

    public string CreateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public string HashRefreshToken(string refreshToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));
        return Convert.ToHexString(hash);
    }
}



//public class TokenService
//{
//    private readonly IConfiguration _configuration;

//    public TokenService(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public (string Token, DateTime ExpiresAt) CreateToken(AppUser user)
//    {
//        var jwtKey = _configuration["Jwt:Key"];
//        var issuer = _configuration["Jwt:Issuer"];
//        var audience = _configuration["Jwt:Audience"];

//        if (string.IsNullOrEmpty(jwtKey))
//            throw new Exception("JWT Key is missing");

//        var claims = new List<Claim>
//    {
//        new Claim(ClaimTypes.Name, user.UserName ?? ""),
//        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
//    };

//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

//        var token = new JwtSecurityToken(
//        issuer: _jwtSettings.Issuer,
//        audience: _jwtSettings.Audience,
//        claims: claims,
//        expires: expiry,          // ← artıq var idi
//        signingCredentials: creds
//    );

//        return (new JwtSecurityTokenHandler().WriteToken(token), expiry);
//    }

//    public string CreateRefreshToken()
//    {
//        return Guid.NewGuid().ToString();
//    }
//}
