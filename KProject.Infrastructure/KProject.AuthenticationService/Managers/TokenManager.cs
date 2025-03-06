using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KProject.DomainModel;
using Microsoft.IdentityModel.Tokens;

namespace KProject.AuthenticationService.Managers
{
    public class TokenManager : ITokenManager
    {
        private const string TokenIdClaimType = "TokenID";
        private const string UserIdClaimType = "UserID";
        private const string InvalidTokenExceptionMessage = "Токен недействительный";

        private readonly IJwtSettingsProvider _jwtSettingsProvider;

        public TokenManager(IJwtSettingsProvider jwtSettingsProvider)
        {
            _jwtSettingsProvider = jwtSettingsProvider;
        }

        public int ExtractUserID(ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == UserIdClaimType)?.Value ??
                throw new Exception(InvalidTokenExceptionMessage);

            if(!int.TryParse(claim, out var userID))
                throw new Exception(InvalidTokenExceptionMessage);

            return userID;
        }

        public Guid ExtractTokenID(ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == TokenIdClaimType)?.Value ??
                throw new Exception(InvalidTokenExceptionMessage);

            if (!Guid.TryParse(claim, out var tokenID))
                throw new Exception(InvalidTokenExceptionMessage);

            return tokenID;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettingsProvider.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(UserIdClaimType, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(TokenIdClaimType, Guid.NewGuid().ToString())
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
