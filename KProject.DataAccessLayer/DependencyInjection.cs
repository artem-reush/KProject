using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace KProject.DataAccessLayer
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавление сервисов доступа к данным в DI контейнер
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<KProjectDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IKProjectDbContext>(provider =>
                provider.GetService<KProjectDbContext>());

            return services;
        }
    }
}
