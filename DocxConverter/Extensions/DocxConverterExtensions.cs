using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocxConverterService.Extensions
{
    public static class DocxConverterExtensions
    {
        public static IServiceCollection AddDocxConverter(this IServiceCollection services,
           IConfiguration docxConverterConfig)
        {
            // Configure DocxConverter service
            //services.Configure<DocxConverterConfiguration>(docxConverterConfig.);

            // Chain pattern
            return services;
        }
    }
}
