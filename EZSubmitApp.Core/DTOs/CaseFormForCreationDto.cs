using System;

namespace EZSubmitApp.Core.DTOs
{
    public abstract class CaseFormForCreationDto
    {
        public virtual string FormType { get; set; }
        public string CaseNumber { get; set; }
        public string HearingDate { get; set; }
        public string HearingTime { get; set; }

        // Plaintiff
        public string PlaintiffType { get; set; }
        public string PlaintiffName { get; set; }
        public string PlaintiffTaDbaType { get; set; }
        public string PlaintiffTaDbaName { get; set; }
        public string PlaintiffAddress1 { get; set; }
        public string PlaintiffAddress2 { get; set; }
        public string PlaintiffPhone { get; set; }

        // Defendant #1
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

        public DateTime GetHearingDateTime()
        {
            return DateTime.Parse(HearingDate + " " + HearingTime);
        }
    }
}
