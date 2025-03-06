namespace KProject.AuthenticationService.Contract
{
    public interface IBlacklistedTokensStorage
    {
        /// <summary>
        /// Добавление токена в черный список
        /// </summary>
        void AddToken(Guid tokenID);

        /// <summary>
        /// Проверка, находится ли токен в черном списке
        /// </summary>
        bool IsTokenBlacklisted(Guid tokenID);
    }
}
