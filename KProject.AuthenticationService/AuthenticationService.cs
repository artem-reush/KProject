using KProject.AuthenticationService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KProject.AuthenticationService.Managers;
using KProject.DataAccessLayer;
using KProject.DomainModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KProject.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IKProjectDbContext _dbContext;
        private readonly IJwtSettingsProvider _jwtSettingsProvider;

        public AuthenticationService(IKProjectDbContext dbContext, IJwtSettingsProvider jwtSettingsProvider)
        {
            _dbContext = dbContext;
            _jwtSettingsProvider = jwtSettingsProvider;
        }

        public async Task<AuthenticationResult> Authenticate(string login, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Login == login);

            if (user is null)
                return AuthenticationResult.Error("Пользователь не найден");

            if (user.IsBlocked)
                return AuthenticationResult.Error("Пользователь заблокирован");

            if (!BcryptHashProvider.Verify(password, user.PasswordHash))
            {
                user.IncreaseLoginAttempts();
                await _dbContext.SaveChangesAsync();
                return AuthenticationResult.Error("Не верный пароль");
            }

            user.ResetLoginAttempts();
            await _dbContext.SaveChangesAsync();

            var token = GenerateJwt(user);

            return AuthenticationResult.Success(token);
        }

        /// <summary>
        /// Генерация JWT токена 
        /// </summary>
        private string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsProvider.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Login)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettingsProvider.Issuer,
                audience: _jwtSettingsProvider.Audience,
                claims: claims,
                expires: DateTime.Now.Add(_jwtSettingsProvider.Expiration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
