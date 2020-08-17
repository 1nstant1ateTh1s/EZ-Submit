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

                //if (!await context.CaseForms.AnyAsync())
                //{
                //    // Create the default case forms
                //    var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                //    await SeedCaseForms(context, userManager);
                //}
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

        private static async Task SeedCaseForms(EZSubmitDbContext context, UserManager<ApplicationUser> userManager)
        {
            // TODO: Pull these email address values from configuration
            string EMAIL_USER = "ambrown@cityofchesapeake.net";

            var firstCaseForm = new WarrantInDebtForm
            {
                CaseNumber = "00199990",
                HearingDateTime = DateTime.Parse("3/18/2021 10:00:00 AM"), 
                PlaintiffType = "I",
                PlaintiffName = "Brown, Andrew M",
                PlaintiffTaDbaType = "DBA",
                PlaintiffTaDbaName = "AB The Good",
                PlaintiffAddress1 = "300 Cedar Rd",
                PlaintiffAddress2 = "Chesapeake, VA 23323",
                PlaintiffPhone = "757-444-4444",
                DefendantType = "I",
                DefendantName = "Sneed, Jason A",
                DefendantTaDbaName = "JAS",
                DefendantAddress1 = "3140 Jason's Rd",
                DefendantAddress2 = "Chesapeake, VA 23321",
                Defendant2Type = "C",
                Defendant2Name = "Monsato",
                Defendant2TaDbaName = "Monsato Inc",
                Defendant2Address1 = "296 Far Out There",
                Defendant2Address2 = "Chesapeake, VA 20090",
                SubmittedBy = await userManager.FindByNameAsync(EMAIL_USER),
                SubmissionDateTime = DateTime.Parse("7/13/2020 3:39:51 PM"),
                TransferredToState = false,
                IsReadyToTransmit = false,
                Principle = 500.50m,
                Interest = 4.30m,
                InterestStartDate = DateTime.Parse("10/21/2019 4:00:00 AM"),
                UseDoj = false,
                FilingCost = 135.87m,
                AttorneyFees = 402.00m,
                AccountType = "Other",
                AccountTypeOther = "An other account type",
                HomesteadExemptionWaived = "Cannot be determined"
                //DocxAttachment = new DocxAttachment
                //{
                //    OutputName = "Generated_Warrant_In_Debt_00199990.docx",
                //    Content = "0x504B0304140006080800000021001E911AB7EB0000004E0200000B0008025F72656C732F2E72656C7320A2040228A0000200000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                //}
            };
            context.CaseForms.Add(firstCaseForm);
            await context.SaveChangesAsync();
        }
    }
}
