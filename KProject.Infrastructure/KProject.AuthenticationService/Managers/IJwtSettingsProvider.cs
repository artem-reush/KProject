namespace KProject.AuthenticationService.Managers
{
    /// <summary>
    /// Интерфейс поставщика настроек JWT
    /// </summary>
    public interface IJwtSettingsProvider
    {
        public string SecretKey { get; }

        public string Issuer { get; }

        public string Audience { get; }

        public TimeSpan Expiration { get; }
    }
}
