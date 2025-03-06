using KProject.AuthenticationService.Contract.Models;

namespace KProject.AuthenticationService.Contract
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        Task<AuthenticationResult> Authenticate(string login, string password);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        Task Registration(string login, string password);
    }
}
