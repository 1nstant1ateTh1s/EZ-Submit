using DocxConverterService.Interfaces;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities.Base;
using EZSubmitApp.Core.Interfaces;
using EZSubmitApp.Core.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EZSubmitApp.Core.Entities
{
    //[JsonConverter(typeof(CaseFormConverterWithTypeDiscriminator<CaseForm>))]
    //public abstract class CaseForm : IntBaseEntity, ICaseForm
    public abstract class CaseForm : IntBaseEntity
    {
        public CaseForm()
        {
        }

        #region Properties
        public virtual string FormType { get; set; }
        public string CaseNumber { get; set; }
        public DateTime HearingDateTime { get; set; }

        // Plaintiff
        public string PlaintiffType { get; set; }
        public string PlaintiffName { get; set; }
        public string PlaintiffTaDbaType { get; set; }
        public string PlaintiffTaDbaName { get; set; }
        public string PlaintiffAddress1 { get; set; }
        public string PlaintiffAddress2 { get; set; }
        public string PlaintiffPhone { get; set; }

        // Defendant #1
        /* TODO: See if "Plaintiffs/Defendants" should be a separate object/table that is reference via foreign key relationship here */
        public string DefendantType { get; set; }
        public string DefendantName { get; set; }
        public string DefendantTaDbaName { get; set; }
        public string DefendantAddress1 { get; set; }
        public string DefendantAddress2 { get; set; }

        // Defendant #2
        public string Defendant2Type { get; set; }
        public string Defendant2Name { get; set; }
        public string Defendant2TaDbaName { get; set; }
        public string Defendant2Address1 { get; set; }
        public string Defendant2Address2 { get; set; }

        // Other metadata
        public ApplicationUser SubmittedBy { get; set; }
        public string SubmittedById { get; set; }
        public DateTime SubmissionDateTime { get; set; }
        public bool TransferredToState { get; set; }
        public DateTime? TransferDateTime { get; set; }
        public bool IsReadyToTransmit { get; set; }

        public DocxAttachment DocxAttachment { get; set; }
        #endregion

        #region Methods
        public abstract IGeneratable ToDocxForm();
        #endregion
    }
}
