namespace KProject.AuthenticationService.Managers
{
    /// <summary>
    /// Провайдер хэширования BCrypt
    /// </summary>
    internal static class BcryptHashProvider
    {
        /// <summary>
        /// Хэширование строки
        /// </summary>
        public static string Hash(string source)
        {
            return BCrypt.Net.BCrypt.HashPassword(source);
        }

        /// <summary>
        /// Проверка строки на соответствие хэшу
        /// </summary>
        public static bool Verify(string source, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(source, hash);
        }
    }
}
