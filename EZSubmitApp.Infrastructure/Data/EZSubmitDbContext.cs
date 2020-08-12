using EZSubmitApp.Core.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace EZSubmitApp.Infrastructure.Data
{
    public class EZSubmitDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public EZSubmitDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        #region Properties
        //public DbSet<Profile> Profiles { get; set; }
        public DbSet<CaseForm> CaseForms { get; set; }
        public DbSet<WarrantInDebtForm> WarrantInDebtForms { get; set; }
        public DbSet<SummonsForUnlawfulDetainerForm> SummonsForUnlawfulDetainerForms { get; set; }
        public DbSet<DocxAttachment> DocxAttachments { get; set; }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base.OnModelCreating() first because EF Core generally has a last-one-wins policy for configuration, 
            // so any customizations should come after
            base.OnModelCreating(modelBuilder);

            // Loading from separate configuration classes
            // Register all entity type configurations in the given assembly automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #endregion
    }
}
