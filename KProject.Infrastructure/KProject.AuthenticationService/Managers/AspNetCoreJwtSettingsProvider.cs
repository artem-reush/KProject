using Microsoft.Extensions.Configuration;

namespace KProject.AuthenticationService.Managers
{
    /// <summary>
    /// Поставщик настроек JWT из ASP.NET Core конфигурации
    /// </summary>
    /// <param name="configuration"></param>
    internal class AspNetCoreJwtSettingsProvider(IConfiguration configuration) : IJwtSettingsProvider
    {
        private readonly string _secretKey = configuration["Jwt:Secret"] 
                                            ?? throw new ArgumentNullException(nameof(SecretKey));
        private readonly string _issuer = configuration["Jwt:Issuer"]
                                          ?? throw new ArgumentNullException(nameof(Issuer));
        private readonly string _audience = configuration["Jwt:Audience"]
                                            ?? throw new ArgumentNullException(nameof(Audience));
        private readonly TimeSpan _expiration = TimeSpan.FromMinutes(int.Parse(
            configuration["Jwt:ExpirationInMinutes"] ?? throw new ArgumentNullException(nameof(Expiration))));

        public string SecretKey => _secretKey;
        public string Issuer => _issuer;
        public string Audience => _audience;
        public TimeSpan Expiration => _expiration;
    }
}
