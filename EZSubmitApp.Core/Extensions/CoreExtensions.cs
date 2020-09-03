using DocxConverterService.Extensions;
using EZSubmitApp.Core.Configuration;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace EZSubmitApp.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services)
        //public static IServiceCollection AddCoreLayer(this IServiceCollection services, ServiceRunSettings serviceRunSettings, string contentRootPath)
        {
            // TODO: FIND OUT IF THERE IS ANOTHER WAY OF GETTING THE ABSOLUTE PATH TO THIS PROJECT AND APPLYING IT TO THE serviceRunSettings.DocxConverterOptionsTemplateLocation
            //      PROPERTY INSTEAD OF RELYING ON STARTUP.CS TO PASS IN THE WebHostEnvironment.ContentRootPath STRING.

            // Register core services w/ DI
            services.AddScoped<ICaseFormService, CaseFormService>();

            //// Configure DocxConverter service
            //// TODO: Test option #1 - applying WebHostEnvironment.ContentRootPath to TemplateLocation in DocxConverterExtensions.cs
            //services.AddDocxConverter(options =>
            //{
            //    //var docxConverterRunSettings = Configuration.Get<DocxConverterRunSettings>();
            //    serviceRunSettings.DocxConverterOptions.TemplateLocation = Path.Combine(
            //        contentRootPath,
            //        serviceRunSettings.DocxConverterOptions.TemplateLocation); // combine WebHostEnvironment.ContentRootPath with TemplateLocation 

            //    options = serviceRunSettings.DocxConverterOptions;
            //});

            //// TODO: Test option #2 - applying WebHostEnvironment.ContentRootPath to TemplateLocation in Startup.cs
            //services.AddDocxConverter(options =>
            //{
            //    //var docxConverterConfig = Configuration.Get<DocxConverterRunSettings>().DocxConverterOptions;
            //    var docxConverterConfig = serviceRunSettings.DocxConverterOptions;
            //    docxConverterConfig.TemplateLocation = Path.Combine(
            //        contentRootPath,
            //        docxConverterConfig.TemplateLocation); // combine WebHostEnvironment.ContentRootPath with TemplateLocation 

            //    options.TemplateLocation = docxConverterConfig.TemplateLocation;
            //    //options.TemplateLocation = Path.Combine(
            //    //    WebHostEnvironment.ContentRootPath,
            //    //    docxConverterConfig.TemplateLocation); // combine WebHostEnvironment.ContentRootPath with TemplateLocation 
            //    options.WarrantInDebtTemplateDocument = docxConverterConfig.WarrantInDebtTemplateDocument;
            //    options.SummonsForUnlawfulDetainerTemplateDocument = docxConverterConfig.SummonsForUnlawfulDetainerTemplateDocument;
            //    options.OutputDirectory = docxConverterConfig.OutputDirectory;
            //    options.CourtName = docxConverterConfig.CourtName;
            //    options.CourtAddress = docxConverterConfig.CourtAddress;
            //});

            // Chain pattern
            return services;
        }
    }
}
