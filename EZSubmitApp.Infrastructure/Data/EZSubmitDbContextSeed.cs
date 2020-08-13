using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EZSubmitApp.Infrastructure.Data
{
    public class EZSubmitDbContextSeed
    {
        public static async Task SeedAsync(EZSubmitDbContext context, IServiceScope serviceScope, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                if (!await context.Roles.AnyAsync())
                {
                    // Create the default roles (if they don't exist yet)
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    await SeedDefaultRoles(roleManager);
                }

                if (!await context.Users.AnyAsync())
                {
                    // Create the default users (if they don't exist yet)
                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                    await SeedDefaultUsers(userManager);
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;

                    // TODO: get logger and log error

                    await SeedAsync(context, serviceScope, retryForAvailability);
                }

                throw;
            }
        }

        private static async Task SeedDefaultRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(AuthorizationConstants.Roles.USER) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.USER));
            }

            if (await roleManager.FindByNameAsync(AuthorizationConstants.Roles.ADMINISTRATOR) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATOR));
            }
        }

        private static async Task SeedDefaultUsers(UserManager<ApplicationUser> userManager)
        {
            // TODO: Pull these email address values from configuration
            string EMAIL_ADMIN = "admin@test.com";
            string EMAIL_USER = "ambrown@cityofchesapeake.net";

            // Check if the admin user already exists
            if (await userManager.FindByNameAsync(EMAIL_ADMIN) == null)
            {
                // Create a new admin ApplicationUser account
                var adminUser = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = EMAIL_ADMIN,
                    Email = EMAIL_ADMIN,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    IsActive = true,
                };

                // Insert the admin user into the DB
                await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);

                // Assign the "RegisteredUser" and "Administrator" roles
                await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.USER);
                await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATOR);
            }

            // Check if the standard user already exists
            if (await userManager.FindByNameAsync(EMAIL_USER) == null)
            {
                // Create a new standard ApplicationUser account
                var defaultUser = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = EMAIL_USER,
                    Email = EMAIL_USER,
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    IsActive = true
                };

                // Insert the standard user into the DB
                await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

                // Assign the "RegisteredUser" role
                await userManager.AddToRoleAsync(defaultUser, AuthorizationConstants.Roles.USER);
            }
        }
    }
}
