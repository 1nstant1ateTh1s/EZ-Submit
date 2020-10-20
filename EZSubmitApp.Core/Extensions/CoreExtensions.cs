using DocxConverterService.Extensions;
using EZSubmitApp.Core.Configuration;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

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

        /// <summary>
        /// Extension method to determine if a given property exists on a type.
        /// </summary>
        /// <param name="obj">The type to inspect.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="throwExceptionIfNotFound">Determines if an exception should be thrown if property is not found.</param>
        /// <returns>True if the property exists, false otherwise.</returns>
        public static bool HasProperty(this Type obj, string propertyName, bool throwExceptionIfNotFound = true)
        {
            var prop = obj.GetProperty(propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (prop == null && throwExceptionIfNotFound)
            {
                throw new NotSupportedException(
                   String.Format("ERROR: Property '{0} does not exist.", propertyName)
                   );
            }

            return prop != null;
        }
    }
}
