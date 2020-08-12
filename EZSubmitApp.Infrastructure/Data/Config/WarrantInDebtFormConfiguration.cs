using EZSubmitApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZSubmitApp.Infrastructure.Data.Config
{
    public class WarrantInDebtFormConfiguration : IEntityTypeConfiguration<WarrantInDebtForm>
    {
        public void Configure(EntityTypeBuilder<WarrantInDebtForm> builder)
        {
            #region Property Configuration Section
            builder.Property(e => e.Principle)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Principle");

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

            builder.Property(e => e.AttorneyFees)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("AttorneyFees");
            #endregion
        }
    }
}
