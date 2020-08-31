using System;

namespace EZSubmitApp.Core.DTOs
{
    public class SummonsForUnlawfulDetainerFormDto : CaseFormDto
    {
        public decimal Principle { get; set; }
        public DateTime RentPeriodStartDate { get; set; }
        public DateTime RentPeriodEndDate { get; set; }
        public decimal LateFee { get; set; }
        public decimal DamagesAmount { get; set; }
        public string DamagesFor { get; set; }
        public decimal Interest { get; set; }
        public DateTime? InterestStartDate { get; set; }
        public bool UseDoj { get; set; }
        public decimal FilingCost { get; set; }
        public decimal CivilRecoveryAmount { get; set; }
        public decimal AttorneyFees { get; set; }
        public bool IsAmountDueAtHearing { get; set; }
        public bool UseToTerminateTenancy { get; set; }
    }
}
