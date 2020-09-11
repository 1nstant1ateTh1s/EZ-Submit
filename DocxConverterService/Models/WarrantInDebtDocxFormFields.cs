
using DocxConverterService.Interfaces;

namespace DocxConverterService.Models
{
    public class WarrantInDebtDocxFormFields
    {
        #region Page 1 Fields
        public string Court { get; set; } = " ";
        public string CourtAddress { get; set; } = " ";
        public string HearingDate { get; set; } = " ";
        public string HearingTime { get; set; } = " ";
        public string Principle { get; set; } = " ";
        public string Interest { get; set; } = " ";
        public string InterestStartDate { get; set; } = " "; // either date string or "DoJ"
        public string FilingCost { get; set; } = " ";
        public string AttorneyFees { get; set; } = " ";

        // TODO: ADD SPECIAL SETTER METHODS TO HANDLE LOGIC OF MAPPING AN INCOMING 'AccountType' VALUE TO EITHER
        //          THE A1, A2, A3, or A4 OPTIONS ??
        //public AccountType AccountType { get; set; }
        /* Represent checkbox options for Account Type on paper form */
        public string A1 { get; set; } = " ";
        public string A2 { get; set; } = " ";
        public string A3 { get; set; } = " ";
        public string A4 { get; set; } = " ";

        public string AccountTypeOther { get; set; } = " ";

        // TODO: ADD SPECIAL SETTER METHODS TO HANDLE LOGIC OF MAPPING AN INCOMING 'HomesteadExemptionWaived' VALUE TO EITHER
        //          THE B1, B2, or B3 OPTIONS ??
        /* Represent checkbox options for Homestead Exemption Waived? on paper form */
        //public HomesteadExemptionWaived HomesteadExemptionWaived { get; set; }
        public string B1 { get; set; } = " ";
        public string B2 { get; set; } = " ";
        public string B3 { get; set; } = " ";

        /* NOTE: These 2 fields are displayed directly underneath the "Homestead Exemption Waived?" section */
        public string Date2 { get; set; } = " ";
        // TODO: ADD SPECIAL SETTER METHODS TO HANDLE LOGIC OF MAPPING AN INCOMING 'SignatureType' VALUE TO EITHER
        //          THE C1, C2, or C3 OPTIONS ??
        /* Represent checkbox options for first occurence of Signature Type on paper form */
        //public SignatureType RB22 { get; set; }
        public string C1 { get; set; } = " ";
        public string C2 { get; set; } = " ";
        public string C3 { get; set; } = " ";

        public string CaseNumber { get; set; } = " ";

        public string PlaintiffName { get; set; } = " ";
        public string PlaintiffAddress1 { get; set; } = " ";
        public string PlaintiffAddress2 { get; set; } = " ";
        public string PlaintiffAddress3 { get; set; } = " ";
        public string PlaintiffPhone { get; set; } = " ";

        public string DefendantName { get; set; } = " ";
        public string DefendantAddress1 { get; set; } = " ";
        public string DefendantAddress2 { get; set; } = " ";
        public string DefendantAddress3 { get; set; } = " ";

        public string AttyForPlaintiff1 { get; set; } = " ";
        public string AttyForPlaintiff2 { get; set; } = " ";
        public string AttyForDefendant1 { get; set; } = " ";
        public string AttyForDefendant2 { get; set; } = " ";
        #endregion

        #region Page 2 Fields
        public string ReturnName1 { get; set; } = " ";
        public string ReturnName1a { get; set; } = " ";
        public string ReturnAddress1 { get; set; } = " ";
        public string ReturnAddress1a { get; set; } = " ";
        public string ReturnPhone1 { get; set; } = " ";

        public string ReturnName2 { get; set; } = " ";
        public string ReturnName2a { get; set; } = " ";
        public string ReturnAddress2 { get; set; } = " ";
        public string ReturnAddress2a { get; set; } = " ";
        public string ReturnPhone2 { get; set; } = " ";

        public string ReturnName3 { get; set; } = " ";
        public string ReturnName3a { get; set; } = " ";
        public string ReturnAddress3 { get; set; } = " ";
        public string ReturnAddress3a { get; set; } = " ";
        public string ReturnPhone3 { get; set; } = " ";

        /* These 2 fields are displayed directly underneath the "Certify" section at the end of page 2 */
        public string Date3 { get; set; } = " ";
        // TODO: ADD SPECIAL SETTER METHODS TO HANDLE LOGIC OF MAPPING AN INCOMING 'SignatureType' VALUE TO EITHER
        //          THE D1, D2, or D3 OPTIONS ??
        /* Represent checkbox options for second occurence of Signature Type on paper form */
        //public SignatureType RB01 { get; set; }
        public string D1 { get; set; } = " ";
        public string D2 { get; set; } = " ";
        public string D3 { get; set; } = " ";
        #endregion
    }

    //public enum AccountType
    //{
    //    OpenAccount = 1,
    //    Contract = 2,
    //    Note = 3,
    //    Other = 4
    //}

    //public enum HomesteadExemptionWaived
    //{
    //    Yes = 1,
    //    No = 2,
    //    CannotBeDetermined = 3
    //}

    //public enum SignatureType
    //{
    //    Plaintiff = 1,
    //    PlaintiffAttorney = 2,
    //    PlaintiffAgent = 3
    //}
}
