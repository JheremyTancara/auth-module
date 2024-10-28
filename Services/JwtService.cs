using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Microsoft.IdentityModel.Tokens;
using Api.Data;

namespace Api.Utilities
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key cannot be null.");
            _issuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer cannot be null.");
            _audience = configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience cannot be null.");
        }

        public string GenerateToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var claims = new[]
            {
                new Claim("UserID", user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static dynamic validarToken(ClaimsIdentity identity, DataContext context)
        {
            try
            {
                if (identity.Claims.Count() == 0) 
                {
                    return new
                    {
                        success = false,
                        message = "Verify if you are sending a valid token",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value;
                if (string.IsNullOrEmpty(id)) 
                {
                    return new
                    {
                        success = false,
                        message = "User ID not found in claims",
                        result = ""
                    };
                }

                var usuario = context.Users.FirstOrDefault(x => x.UserID.ToString() == id);
                
                return new
                {
                    success = true,
                    message = "Success",
                    result = usuario
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Exception: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
