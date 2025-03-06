using Microsoft.Extensions.DependencyInjection;
using KProject.TransactionsService.Contract;

namespace KProject.TransactionsService
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавление сервиса аутентификации в DI контейнер
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddTransactionsService(this IServiceCollection services)
        {
            services.AddScoped<ITransactionsService, TransactionsService>();
            return services;
        }
    }
}
