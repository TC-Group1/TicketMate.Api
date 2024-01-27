using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace TicketMate.Persistence.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection InjectPersistenceDependencies(this IServiceCollection services, string? connectionString)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new NullReferenceException(nameof(connectionString));
            }

            services.AddSingleton<IDbConnectionFactory>(new MySqlConnectionFactory(connectionString));

            services.AddSingleton<IDataAccess, DataAccess>();

            return services;
        }
    }
}
