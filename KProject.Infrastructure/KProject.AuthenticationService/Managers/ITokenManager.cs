using System.Security.Claims;
using KProject.DomainModel;

namespace KProject.AuthenticationService.Managers
{
    public interface ITokenManager
    {
        /// <summary>
        /// Извлечение идентификатора пользователя из токена
        /// </summary>
        int ExtractUserID(ClaimsPrincipal user);

        /// <summary>
        /// Извлечение идентификатора токена из токена
        /// </summary>
        Guid ExtractTokenID(ClaimsPrincipal user);

        /// <summary>
        /// Генерация токена
        /// </summary>
        string GenerateToken(User user);
    }
}
