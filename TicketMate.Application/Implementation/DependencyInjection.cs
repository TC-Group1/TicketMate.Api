using Microsoft.Extensions.DependencyInjection;
using TicketMate.Application.Abstraction;
using System.Diagnostics.CodeAnalysis;

namespace TicketMate.Application.Implementation
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new NullReferenceException(nameof(services));
            }

            services.AddSingleton<IHandlerDictionary>(new HandlerDictionary());

            services.AddScoped<IOrchestrator, Orchestrator>();

            services.AddScoped<IHandlerFactory, HandlerFactory>();

            services.AddTransient<IServiceProvider>(_ => services.BuildServiceProvider());

            services.AddTransient<ITypeActivator, TypeActivator>();

            return services;
        }
    }
}
