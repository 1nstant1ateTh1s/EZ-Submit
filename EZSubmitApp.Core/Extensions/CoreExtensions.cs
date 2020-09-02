using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EZSubmitApp.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register service dependencies w/ DI
            services.AddScoped<ICaseFormService, CaseFormService>();

            // Chain pattern
            return services;
        }
    }
}
