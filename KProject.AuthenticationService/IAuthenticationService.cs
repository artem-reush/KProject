using KProject.AuthenticationService.Contract;

namespace KProject.AuthenticationService
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        Task<AuthenticationResult> Authenticate(string login, string password);
    }
}
