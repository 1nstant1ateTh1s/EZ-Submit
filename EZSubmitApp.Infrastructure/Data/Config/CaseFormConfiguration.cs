using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EZSubmitApp.Infrastructure.Data.Config
{
    public class CaseFormConfiguration : BaseEntityConfiguration<CaseForm>
    {
        public override void Configure(EntityTypeBuilder<CaseForm> builder)
        {
            base.Configure(builder); // must call this

            // Remaining configurations here
            #region Type Configuration Section
            builder.ToTable("CaseForm")
                .HasDiscriminator<string>("FormType")
                .HasValue<WarrantInDebtForm>(CaseFormTypes.WARRANT_IN_DEBT)
                .HasValue<SummonsForUnlawfulDetainerForm>(CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER);

            builder.HasOne(c => c.SubmittedBy)
                .WithMany(u => u.CaseForms)
                .HasForeignKey(c => c.SubmittedById); // one-to-many relationship between user and case forms

            builder.HasOne(c => c.DocxAttachment)
                .WithOne(d => d.CaseForm)
                .HasForeignKey<DocxAttachment>(d => d.CaseFormId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false); // one-to-one relationship between case form and docxattachment

            #endregion

            #region Property Configuration Section
            builder.Property(e => e.FormType)
                .IsRequired();

            builder.Property(e => e.CaseNumber)
                .IsRequired();

            builder.Property(e => e.HearingDateTime)
                .IsRequired();

            // Plaintiff
            builder.Property(e => e.PlaintiffType)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.PlaintiffName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.PlaintiffTaDbaType)
                .HasMaxLength(3);

            builder.Property(e => e.PlaintiffTaDbaName)
                .HasMaxLength(50);

            builder.Property(e => e.PlaintiffAddress1)
               .HasMaxLength(50);

            builder.Property(e => e.PlaintiffAddress2)
               .HasMaxLength(50);

            // Defendant #1
            builder.Property(e => e.DefendantType)
               .IsRequired()
               .HasMaxLength(1);

            builder.Property(e => e.DefendantName)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(e => e.DefendantTaDbaName)
                .HasMaxLength(50);

            builder.Property(e => e.DefendantAddress1)
               .HasMaxLength(50);

            builder.Property(e => e.DefendantAddress2)
               .HasMaxLength(50);

            // Defendant #2
            builder.Property(e => e.Defendant2Type)
               .HasMaxLength(1);

            builder.Property(e => e.Defendant2Name)
               .HasMaxLength(150);

            builder.Property(e => e.Defendant2TaDbaName)
                .HasMaxLength(50);

            builder.Property(e => e.Defendant2Address1)
               .HasMaxLength(50);

            builder.Property(e => e.Defendant2Address2)
               .HasMaxLength(50);

            /* TODO: SET MAXLENGTH / REGEX FOR REMAINING PROPERTIES: 
            * 
            *      Plaintiff Phone Number
            */

            // Other metadata
            builder.Property(e => e.SubmissionDateTime)
                .HasDefaultValueSql("GetDate()");
            #endregion

        }
    }

}
