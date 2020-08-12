using EZSubmitApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZSubmitApp.Infrastructure.Data.Config
{
    public class SummonsForUnlawfulDetainerFormConfiguration : IEntityTypeConfiguration<SummonsForUnlawfulDetainerForm>
    {
        public void Configure(EntityTypeBuilder<SummonsForUnlawfulDetainerForm> builder)
        {
            #region Property Configuration Section
            builder.Property(s => s.Principle)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Principle");

            builder.Property(s => s.LateFee)
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.DamagesAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.DamagesFor)
                .HasMaxLength(20);

            builder.Property(e => e.Interest)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Interest");

            builder.Property(e => e.InterestStartDate)
                .HasColumnName("InterestStartDate");

            builder.Property(e => e.UseDoj)
                .HasColumnName("UseDoj");

            builder.Property(e => e.FilingCost)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("FilingCost");

            builder.Property(e => e.CivilRecoveryAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.AttorneyFees)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("AttorneyFees");
            #endregion
        }
    }
}
