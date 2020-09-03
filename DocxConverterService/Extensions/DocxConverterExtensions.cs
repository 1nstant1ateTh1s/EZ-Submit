using DocxConverterService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DocxConverterService.Extensions
{
    public static class DocxConverterExtensions
    {
        public static IServiceCollection AddDocxConverter(this IServiceCollection services,
          Action<DocxConverterOptions> options)
        {
            // Configure DocxConverter service
            services.AddScoped<IDocxConverter, DocxConverter>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Please provide options for DocxConverter Service.");
            }
            services.Configure(options);

            // Chain pattern
            return services;
        }
    }
}
