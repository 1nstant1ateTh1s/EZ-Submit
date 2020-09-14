using AutoMapper;
using DocxConverterService.Models;
using EZSubmitApp.Core.Constants;
using EZSubmitApp.Core.Entities;
using EZSubmitApp.Core.Extensions;
using System;

namespace EZSubmitApp.Core.Mapper.Converters
{
    public class WDDocxTypeConverter : ITypeConverter<WarrantInDebtForm, WarrantInDebtDocxForm>
    {
        public WarrantInDebtDocxForm Convert(WarrantInDebtForm source, WarrantInDebtDocxForm destination, ResolutionContext context)
        {
            var court = context.Items["court"].ToString();
            var courtAddress = context.Items["courtAddress"].ToString();

            destination = new WarrantInDebtDocxForm();

            destination.Fields.Court = court;
            destination.Fields.CourtAddress = courtAddress;
            destination.Fields.HearingDate = source.HearingDateTime.ToString("MM/dd/yyyy");
            destination.Fields.HearingTime = source.HearingDateTime.ToString("hh:mm tt");
            destination.Fields.Principle = source.Principle.ToString();
            destination.Fields.Interest = source.Interest.ToString();
            destination.Fields.InterestStartDate = source.UseDoj ? "DOJ" : source.InterestStartDate.ToString("MM/dd/yyyy");
            destination.Fields.FilingCost = source.FilingCost.ToString();
            destination.Fields.AttorneyFees = source.AttorneyFees.ToString();
            destination.Fields.A1 = (source.AccountType == AccountTypes.OPEN_ACCOUNT ? "X" : "");
            destination.Fields.A2 = (source.AccountType == AccountTypes.CONTRACT ? "X" : "");
            destination.Fields.A3 = (source.AccountType == AccountTypes.NOTE ? "X" : "");
            destination.Fields.A4 = (source.AccountType == AccountTypes.OTHER ? "X" : "");
            destination.Fields.AccountTypeOther = source.AccountTypeOther ?? "";
            destination.Fields.B1 = (source.HomesteadExemptionWaived == HomesteadExemptionWaived.YES ? "X" : "");
            destination.Fields.B2 = (source.HomesteadExemptionWaived == HomesteadExemptionWaived.NO ? "X" : "");
            destination.Fields.B3 = (source.HomesteadExemptionWaived == HomesteadExemptionWaived.CANNOT_BE_DETERMINED ? "X" : "");
            destination.Fields.Date2 = ""; // NOTE: not currently represented on front-end form
            destination.Fields.C1 = ""; // NOTE: not currently represented on front-end form
            destination.Fields.C2 = ""; // NOTE: not currently represented on front-end form
            destination.Fields.C3 = ""; // NOTE: not currently represented on front-end form
            destination.Fields.CaseNumber = source.CaseNumber;
            destination.Fields.PlaintiffName = source.PlaintiffName;
            destination.Fields.PlaintiffAddress1 = source.PlaintiffTaDbaName;
            destination.Fields.PlaintiffAddress2 = source.PlaintiffAddress1;
            destination.Fields.PlaintiffAddress3 = source.PlaintiffAddress2;
            destination.Fields.PlaintiffPhone = source.PlaintiffPhone;
            destination.Fields.DefendantName = source.DefendantName;
            destination.Fields.DefendantAddress1 = String.Format("{0} {1}", source.DefendantAddress1, source.DefendantAddress2);
            destination.Fields.DefendantAddress2 = source.Defendant2Name;
            destination.Fields.DefendantAddress3 = String.Format("{0} {1}", source.Defendant2Address1, source.Defendant2Address2);

            // TODO: Need to get profile / attorney info for here
            destination.Fields.AttyForPlaintiff1 = "";
            destination.Fields.AttyForPlaintiff2 = "";
            destination.Fields.AttyForDefendant1 = "";
            destination.Fields.AttyForDefendant2 = "";

            // NOTE: None of these fields are currently represented on front-end form
            destination.Fields.ReturnName1 = "";
            destination.Fields.ReturnName1a = "";
            destination.Fields.ReturnAddress1 = "";
            destination.Fields.ReturnAddress1a = "";
            destination.Fields.ReturnPhone1 = "";
            destination.Fields.ReturnName2 = "";
            destination.Fields.ReturnName2a = "";
            destination.Fields.ReturnAddress2 = "";
            destination.Fields.ReturnAddress2a = "";
            destination.Fields.ReturnPhone2 = "";
            destination.Fields.ReturnName3 = "";
            destination.Fields.ReturnName3a = "";
            destination.Fields.ReturnAddress3 = "";
            destination.Fields.ReturnAddress3a = "";
            destination.Fields.ReturnPhone3 = "";
            destination.Fields.Date3 = "";
            destination.Fields.D1 = "";
            destination.Fields.D2 = "";
            destination.Fields.D3 = "";

            return destination;
        }
    }
}
