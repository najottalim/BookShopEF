using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Domain.Entites.Users;
using BookStore.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Service.Services;

public class AuthManager : IAuthManager
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly double _expire;
    

    public AuthManager(IConfiguration configuration)
    {
        _key = configuration.GetSection("Jwt:Key").Value!;
        _expire = double.Parse(configuration.GetSection("Jwt:Expire").Value!);
        _issuer = configuration.GetSection("Jwt:Issuer").Value!;
        _audience = configuration.GetSection("Jwt:Audience").Value!;
    }
    
    public string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserRole.ToString())
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(_expire),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}