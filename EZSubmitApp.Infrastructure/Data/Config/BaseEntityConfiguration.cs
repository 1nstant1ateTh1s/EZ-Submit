using EZSubmitApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZSubmitApp.Infrastructure.Data.Config
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> 
        where T : IntBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            #region Type Configuration Section
            builder.HasKey(e => e.Id);
            #endregion

            #region Property Configuration Section
            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("GetDate()")
                .ValueGeneratedOnUpdate();
            #endregion
        }
    }
}
