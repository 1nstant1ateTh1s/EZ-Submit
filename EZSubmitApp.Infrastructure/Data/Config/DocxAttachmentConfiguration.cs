using EZSubmitApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZSubmitApp.Infrastructure.Data.Config
{
    public class DocxAttachmentConfiguration : BaseEntityConfiguration<DocxAttachment>
    {
        public override void Configure(EntityTypeBuilder<DocxAttachment> builder)
        {
            base.Configure(builder); // must call this

            // Remaining configurations here
            #region Type Configuration Section
            builder.ToTable("DocxAttachment");
            #endregion

            #region Property Configuration Section
            #endregion
        }
    }
}
