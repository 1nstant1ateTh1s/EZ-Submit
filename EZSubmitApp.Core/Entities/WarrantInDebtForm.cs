using DocxConverterService.Interfaces;
using EZSubmitApp.Core.Constants;
using System;

namespace EZSubmitApp.Core.Entities
{
    public class WarrantInDebtForm : CaseForm
    {
        public WarrantInDebtForm()
        {
        }

        #region Properties
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
        #endregion

        #region Methods
        public override IGeneratable ToDocxForm()
        {
            // If I implement this, it will be responsible for calling the appropriate _mapper.Map method for WD case forms
            throw new NotImplementedException();
        }
        #endregion
    }
}
