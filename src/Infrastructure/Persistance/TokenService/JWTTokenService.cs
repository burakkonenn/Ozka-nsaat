using Application.Abstractions.Wrappers;
using Application.DTOs.User;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistance.Context;
using Persistance.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistance.TokenService
{
    public class JWTTokenService : IJWTTokenServices
    {

        private readonly IConfiguration _configuration;
        private readonly OzkaDbContext _context;

        public JWTTokenService(IConfiguration configuration, OzkaDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        public JWTToken Authenticate(User user)
        {
            if (!_context.User.Any(e => e.UserName == user.UserName && e.Password == user.Password))
            {
                return null;
            }

            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWTToken:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),

                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);

            return new JWTToken { Token = tokenhandler.WriteToken(token) };
        }
    }
}
