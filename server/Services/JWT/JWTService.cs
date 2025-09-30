using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rodnie.API.Models;

namespace Rodnie.API.Services.JWT {
    public class JWTService : IJWTService {
        private readonly IConfiguration config;

        public JWTService(IConfiguration config) {
            this.config = config;
        }

        public string GenerateToken(User user) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Role, user.role.ToString())
            };
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(config["JWT:LifetimeMinutes"]));

            var token = new JwtSecurityToken(signingCredentials: creds, claims: claims, expires: expires);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
