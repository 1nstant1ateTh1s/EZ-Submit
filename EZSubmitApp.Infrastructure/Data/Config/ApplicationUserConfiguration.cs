using EZSubmitApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZSubmitApp.Infrastructure.Data.Config
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            #region Type Configuration Section
            //builder.HasOne(u => u.Profile)
            //    .WithOne(p => p.User)
            //    .HasForeignKey<Profile>(p => p.UserId); // one-to-one relationship between user and profile
            #endregion

            #region Property Configuration Section
            builder.Property(u => u.IsActive)
                .IsRequired();
            #endregion
        }
    }
}
