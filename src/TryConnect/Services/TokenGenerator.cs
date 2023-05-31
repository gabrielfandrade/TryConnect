using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TryConnect.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TryConnect.Constants;

namespace TryConnect.Services
{
    public class TokenGenerator
    {
        public string Generate(Student student)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(student),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddDays(1)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity AddClaims(Student student)
        {
            var claims = new ClaimsIdentity();

            var nameClaim = new Claim(ClaimTypes.Name, student.Name);
            var emailClaim = new Claim(ClaimTypes.Email, student.Email);
            
            var claimsCollection = new List<Claim>();

            claimsCollection.Add(nameClaim);
            claimsCollection.Add(emailClaim);

            claims.AddClaims(claimsCollection);

            return claims;
        }
    }
}