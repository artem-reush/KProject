namespace KProject.AuthenticationService.Managers
{
    /// <summary>
    /// Интерфейс менеджера хэширования
    /// </summary>
    internal interface IHashProvider
    {
        /// <summary>
        /// Хэширование строки
        /// </summary>
        string Hash(string source);

        /// <summary>
        /// Проверка строки на соответствие хэшу
        /// </summary>
        bool Verify(string source, string hash);
    }
}
