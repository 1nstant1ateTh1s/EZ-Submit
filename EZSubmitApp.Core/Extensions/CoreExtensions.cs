using DocxConverterService.Extensions;
using EZSubmitApp.Core.Configuration;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace EZSubmitApp.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomConfiguration(configuration);

            // Register core services w/ DI
            services.AddScoped<ICaseFormService, CaseFormService>();

            //// Configure DocxConverter service

            // Chain pattern
            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ServiceRunSettings>(configuration);

            return services;
        }

        /// <summary>
        /// Extension method to allow formatting when calling .ToString on nullable DateTime.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this DateTime? dt, string format)
        {
            return (dt == null ? "" : ((DateTime)dt).ToString(format));
        }
    }
}
