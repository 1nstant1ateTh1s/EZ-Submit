using DocxConverterService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocxConverterService.Extensions
{
    public static class DocxConverterExtensions
    {
        public static IServiceCollection AddDocxConverter(this IServiceCollection services,
           Action<DocxConverterOptions> options, string contentRootPath)
        {
            // Configure DocxConverter service
            services.AddScoped<IDocxConverter, DocxConverter>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Please provide options for DocxConverter Service.");
            }
            //services.Configure(options);
            //services.PostConfigure<DocxConverterOptions>(o =>
            //{
            //    //o.TemplateLocation = Path.Combine(
            //    //    contentRootPath,
            //    //    String.Format(Configuration.GetValue<string>("DocxConverterConfiguration:TemplateLocation")));
            //    o.TemplateLocation = Path.Combine(
            //       contentRootPath,
            //       String.Format(options));
            //});
            services.Configure(options)
                .PostConfigureAll<DocxConverterOptions>(o => 
            {
                o.TemplateLocation = Path.Combine(
                    contentRootPath,
                    String.Format(o.TemplateLocation));
            });

            // Chain pattern
            return services;
        }

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
