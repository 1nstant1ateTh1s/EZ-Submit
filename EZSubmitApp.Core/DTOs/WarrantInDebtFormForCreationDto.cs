using EZSubmitApp.Core.Constants;
using System;

namespace EZSubmitApp.Core.DTOs
{
    public class WarrantInDebtFormForCreationDto : CaseFormForCreationDto
    {
        public override string FormType { get; set; } = CaseFormTypes.WARRANT_IN_DEBT;
        public decimal Principle { get; set; }
        public decimal Interest { get; set; }
        public DateTime? InterestStartDate { get; set; }
        public bool UseDoj { get; set; }
        public decimal FilingCost { get; set; }
        public decimal AttorneyFees { get; set; }
        public string AccountType { get; set; }
        public string AccountTypeOther { get; set; }
        public string HomesteadExemptionWaived { get; set; }
    }
}
