using Microsoft.Extensions.DependencyInjection;

namespace KProject.AuthenticationService
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавление сервиса аутентификации в DI контейнер
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //services.AddSingleton<IToeknManager, SimpleInMemoryTokenManager>();

            return services;
        }
    }
}
