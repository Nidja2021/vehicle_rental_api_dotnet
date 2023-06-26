using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Helpers
{
    public class AuthClass
    {
        public static string GenerateToken<TEntity>(TEntity entity, IConfiguration _configuration)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, AuthClass.GetValueOfProperty(entity, "Id")),
                new Claim(ClaimTypes.Role, AuthClass.GetValueOfProperty(entity, "Role"))
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:SecretKey").Value!
            ));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: _configuration.GetSection("JWT:ValidIssuer").Value!,
                audience: _configuration.GetSection("JWT:ValidAudience").Value!,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private static string GetValueOfProperty<TEntity>(TEntity entity, string propertyName)
        {
            var entityType = entity?.GetType();
            var property = entityType?.GetProperty(propertyName);
            return property?.GetValue(entity)?.ToString()!;
        }

        public static async Task<TEntity?> GetEntityByEmailAsync<TEntity>(DataContext context, string email)
            where TEntity : class
        {
            
            if (typeof(TEntity) == typeof(User))
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Email == email) as TEntity;
            }

            return null;
        }
    }
}