namespace KProject.AuthenticationService.Contract.Models
{
    public class AuthenticationResult
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        /// <summary>
        /// Возвращает результат аутентификации с ошибкой
        /// </summary>
        public static AuthenticationResult Error(string message)
        {
            return new AuthenticationResult { IsOk = false, Message = message };
        }

        /// <summary>
        /// Возвращает результат аутентификации с токеном
        /// </summary>
        /// <param name="token">Токен</param>
        public static AuthenticationResult Success(string token)
        {
            return new AuthenticationResult { IsOk = true, Token = token };
        }
    }
}
