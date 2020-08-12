using EZSubmitApp.Core.Constants;
using System;

namespace EZSubmitApp.Core.Entities
{
    public class SummonsForUnlawfulDetainerForm : CaseForm
    {
        public SummonsForUnlawfulDetainerForm()
        {
        }

        #region Properties
        public override string FormType { get; set; } = CaseFormTypes.SUMMONS_FOR_UNLAWFUL_DETAINER;
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
        #endregion
    }
}
