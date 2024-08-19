using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendPruebaTecnica.Context;
using BackendPruebaTecnica.Models;
namespace BackendPruebaTecnica.Custom
{
    public class Utilities
    {
        private readonly IConfiguration _configuration;
        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string encryptSHA256(string text) 
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //Se computa el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                //Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i=0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string generateJWT(User user)
        {
            //Crear la informacion del usuario para token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);

            //Crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims:userClaims,
                expires:DateTime.UtcNow.AddDays(1),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
