using EZSubmitApp.Core.Configuration;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.IRepositories.Base;
using EZSubmitApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace EZSubmitApp.Infrastructure.Extensions
{
    public static class DBExtensions
    {
        public static IServiceCollection AddDbLayer(this IServiceCollection services, 
            string connectionString)
        {
            // Add ApplicationDbContext
            services.AddDbContext<EZSubmitDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add ASP.NET Core Identity support
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EZSubmitDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, EZSubmitDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAllRepositories(typeof(DBExtensions).Assembly);

            // Chain pattern
            return services;
        }

        public static async void UseDbLayer(this IApplicationBuilder app, IServiceProvider serviceProvider, AspnetRunSettings aspnetRunSettings)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<EZSubmitDbContext>();

                // TODO: Microsoft recommends NOT using this command for production environments due to possible conflicts if the app
                //      is ever scaled out and running on multiple servers/instances/etc. But how to ensure the database is in place
                //      before trying to Seed?
                // context.Database.Migrate();

                await EZSubmitDbContextSeed.SeedAsync(context, serviceScope, aspnetRunSettings);
            }
        }

        /// <summary>
        /// Registers repository services found within the specified assembly.
        /// </summary>
        /// <param name="services">The service collection being extended.</param>
        /// <param name="assembly">The target assembly for repository services to register.</param>
        /// <returns>Chained pattern for service collection.</returns>
        public static IServiceCollection AddAllRepositories
           (this IServiceCollection services, Assembly assembly)
        {
            // We will all register concrete repositories that are of type IRepository
            var repositories = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(IRepository).IsAssignableFrom(x));
            foreach (var repository in repositories)
            {
                var repositoryInterface = repository.GetInterfaces()
                    .Where(i => !i.IsGenericType && typeof(IRepository) != i
                            && typeof(IRepository).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (repositoryInterface != null)
                {
                    services.AddScoped(repositoryInterface, repository);
                }
            }
            return services;
        }
    }
}
