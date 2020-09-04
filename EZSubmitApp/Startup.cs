using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EZSubmitApp.Infrastructure.Data;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Infrastructure.Extensions;
using System;
using System.IO;
using DocxConverterService.Extensions;
using DocxConverterService.Interfaces;
using DocxConverterService;
using EZSubmitApp.Core.Configuration;
using EZSubmitApp.Core.JsonConverters;
using System.Text.Json;
using EZSubmitApp.Core.Constants;
using System.Collections.Generic;
using AutoMapper;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.Services;
using EZSubmitApp.Core.Extensions;

namespace EZSubmitApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
            AspnetRunSettings = configuration.Get<AspnetRunSettings>();
            ServiceRunSettings = configuration.Get<ServiceRunSettings>();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public AspnetRunSettings AspnetRunSettings { get; }
        public ServiceRunSettings ServiceRunSettings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbLayer(AspnetRunSettings.ConnectionStrings.DefaultConnection);
            //services.AddCoreLayer(ServiceRunSettings, WebHostEnvironment.ContentRootPath);
            services.AddCoreLayer();

            // Configure DocxConverter service
            // TODO: Test option #2
            services.AddDocxConverter(options =>
            {
                ServiceRunSettings.DocxConverterOptions.TemplateLocation = Path.Combine(
                    WebHostEnvironment.ContentRootPath,
                    ServiceRunSettings.DocxConverterOptions.TemplateLocation); // combine WebHostEnvironment.ContentRootPath with TemplateLocation 

                options = ServiceRunSettings.DocxConverterOptions;
            });

            //// TODO: Test option #2
            //services.AddDocxConverter(options =>
            //{
            //    var docxConverterConfig = Configuration.Get<ServiceRunSettings>().DocxConverterOptions;
            //    docxConverterConfig.TemplateLocation = Path.Combine(
            //        WebHostEnvironment.ContentRootPath,
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

            //services.Configure<DocxConverterOptions>(Configuration.GetSection("DocxConverterConfiguration"));
            //services.PostConfigure<DocxConverterOptions>(o =>
            //{
            //    o.TemplateLocation = Path.Combine(
            //        WebHostEnvironment.ContentRootPath,
            //        String.Format(Configuration.GetValue<string>("DocxConverterConfiguration:TemplateLocation")));
            //});
            //services.AddScoped<IDocxConverter, DocxConverter>();

            // TODO: Move this to Core project
            //services.AddScoped<ICaseFormService, CaseFormService>();

            services.AddControllersWithViews()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.Converters.Add(new CaseFormJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new CaseFormDtoJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new CaseFormForCreationDtoJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new CaseFormForUpdateDtoJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new DecimalJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new NullableDateTimeJsonConverter());
                });

            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    // Retrieve cache configuration from appsettings.json
                    context.Context.Response.Headers["Cache-Control"] =
                        Configuration["StaticFiles:Headers:Cache-Control"];
                    context.Context.Response.Headers["Pragma"] =
                        Configuration["StaticFiles:Headers:Pragma"];
                    context.Context.Response.Headers["Expires"] =
                        Configuration["StaticFiles:Headers:Expires"];
                }
            });

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseDbLayer(serviceProvider, AspnetRunSettings);

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // Starts an instance of Angular CLI server in the background when the app starts in development mode.
                    //spa.UseAngularCliServer(npmScript: "start");

                    // This tells the app to use the external Angular CLI instance instead of launching one of its own. (*This also means the 
                    //  developer needs to make sure and start it separately).
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
