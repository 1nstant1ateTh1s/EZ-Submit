
namespace EZSubmitApp.Core.ResourceParameters
{
    public class CaseFormParameters : QueryStringParameters
    {
        //public CaseFormParameters()
        //{
        //    SortColumn = "caseNumber";
        //}

        public string CaseNumber { get; set; }
        public bool HideTransmitted { get; set; }
        public string MinHearingDate { get; set; }
        public string MaxHearingDate { get; set; }
        public string SubmittedBy { get; set; }

        public string Search { get; set; }

    }
}
