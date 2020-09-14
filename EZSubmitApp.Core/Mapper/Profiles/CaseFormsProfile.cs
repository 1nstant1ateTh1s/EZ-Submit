using AutoMapper;
using DocxConverterService.Models;
using EZSubmitApp.Core.Mapper.Converters;
using EZSubmitApp.Core.Mapper.Resolvers;
using EZSubmitApp.Core.Extensions;

namespace EZSubmitApp.Core.Mapper.Profiles
{
    public class CaseFormsProfile : Profile
    {
        public CaseFormsProfile()
        {
            #region Normal DTO mappings
            // NOTE: AutoMapper understands runtime polymorphic mappings
            CreateMap<Entities.CaseForm, DTOs.CaseFormDto>()
                .Include<Entities.WarrantInDebtForm, DTOs.WarrantInDebtFormDto>()
                .Include<Entities.SummonsForUnlawfulDetainerForm, DTOs.SummonsForUnlawfulDetainerFormDto>();
            CreateMap<Entities.WarrantInDebtForm, DTOs.WarrantInDebtFormDto>();
            CreateMap<Entities.SummonsForUnlawfulDetainerForm, DTOs.SummonsForUnlawfulDetainerFormDto>();
            #endregion

            #region Creation DTO mappings
            CreateMap<DTOs.CaseFormForCreationDto, Entities.CaseForm>()
               .ForMember(dest => dest.HearingDateTime, opt => opt.MapFrom(src => src.GetHearingDateTime()))
               .Include<DTOs.WarrantInDebtFormForCreationDto, Entities.WarrantInDebtForm>()
               .Include<DTOs.SummonsForUnlawfulDetainerFormForCreationDto, Entities.SummonsForUnlawfulDetainerForm>();
            CreateMap<DTOs.WarrantInDebtFormForCreationDto, Entities.WarrantInDebtForm>();
            CreateMap<DTOs.SummonsForUnlawfulDetainerFormForCreationDto, Entities.SummonsForUnlawfulDetainerForm>();
            #endregion
            
            #region Update DTO mappings
            CreateMap<DTOs.CaseFormForUpdateDto, Entities.CaseForm>()
                .ForMember(dest => dest.HearingDateTime, opt => opt.MapFrom(src => src.GetHearingDateTime()))
                .Include<DTOs.WarrantInDebtFormForUpdateDto, Entities.WarrantInDebtForm>()
                .Include<DTOs.SummonsForUnlawfulDetainerFormForUpdateDto, Entities.SummonsForUnlawfulDetainerForm>();
            CreateMap<DTOs.WarrantInDebtFormForUpdateDto, Entities.WarrantInDebtForm>();
            CreateMap<DTOs.SummonsForUnlawfulDetainerFormForUpdateDto, Entities.SummonsForUnlawfulDetainerForm>();
            #endregion

            #region Docx conversion mappings
            // TODO: Better method of handling this mapping ...
            CreateMap<Entities.WarrantInDebtForm, WarrantInDebtDocxForm>()
                // custom converter
                .ConvertUsing<WDDocxTypeConverter>();

            CreateMap<Entities.WarrantInDebtForm, WarrantInDebtDocxFormFields>()
                .ForMember(dest => dest.Court, opt => opt.MapFrom(src => "CHESAPEAKE")) // TODO: READ FROM CONFIGURATION
                .ForMember(dest => dest.CourtAddress, opt => opt.MapFrom(src => "307 Albemarle Drive, Suite 200B, Chesapeake, VA 23322, PH-757-382-3143 FAX-757-382-3113")) // TODO: READ FROM CONFIGURATION
                .ForMember(dest => dest.HearingDate, opt => opt.MapFrom(src => src.HearingDateTime.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.HearingTime, opt => opt.MapFrom(src => src.HearingDateTime.ToString("hh:mm tt")))
                .ForMember(dest => dest.Principle, opt => opt.MapFrom(src => src.Principle.ToString()))
                .ForMember(dest => dest.Interest, opt => opt.MapFrom(src => src.Interest.ToString()))
                .ForMember(dest => dest.InterestStartDate, opt => opt.MapFrom(src => src.UseDoj ? "DOJ" : src.InterestStartDate.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.FilingCost, opt => opt.MapFrom(src => src.FilingCost.ToString()))
                .ForMember(dest => dest.AttorneyFees, opt => opt.MapFrom(src => src.AttorneyFees.ToString()))
                .ForMember(dest => dest.A1, opt => opt.MapFrom<A1TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.A2, opt => opt.MapFrom<A2TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.A3, opt => opt.MapFrom<A3TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.A4, opt => opt.MapFrom<A4TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.AccountTypeOther, opt => opt.MapFrom(src => src.AccountTypeOther))
                .ForMember(dest => dest.B1, opt => opt.MapFrom<B1TypeResolver, string>(src => src.HomesteadExemptionWaived))
                .ForMember(dest => dest.B2, opt => opt.MapFrom<B2TypeResolver, string>(src => src.HomesteadExemptionWaived))
                .ForMember(dest => dest.B3, opt => opt.MapFrom<B3TypeResolver, string>(src => src.HomesteadExemptionWaived))
                .ForMember(dest => dest.Date2, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.C1, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.C2, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.C3, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
                .ForMember(dest => dest.PlaintiffName, opt => opt.MapFrom(src => src.PlaintiffName))
                .ForMember(dest => dest.PlaintiffAddress1, opt => opt.MapFrom(src => src.PlaintiffTaDbaName))
                .ForMember(dest => dest.PlaintiffAddress2, opt => opt.MapFrom(src => src.PlaintiffAddress1))
                .ForMember(dest => dest.PlaintiffAddress3, opt => opt.MapFrom(src => src.PlaintiffAddress2))
                .ForMember(dest => dest.PlaintiffPhone, opt => opt.MapFrom(src => src.PlaintiffPhone))
                .ForMember(dest => dest.DefendantName, opt => opt.MapFrom(src => src.DefendantName))
                .ForMember(dest => dest.DefendantAddress1, opt => opt.MapFrom(src => string.Format("{0} {1}", src.DefendantAddress1, src.DefendantAddress2)))
                .ForMember(dest => dest.DefendantAddress2, opt => opt.MapFrom(src => src.Defendant2Name))
                .ForMember(dest => dest.DefendantAddress3, opt => opt.MapFrom(src => string.Format("{0} {1}", src.Defendant2Address1, src.Defendant2Address2)))

                // TODO: Need to get profile / attorney info for here
                .ForMember(dest => dest.AttyForPlaintiff1, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.AttyForPlaintiff2, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.AttyForDefendant1, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.AttyForDefendant2, opt => opt.MapFrom(src => ""))

                // NOTE: None of these fields are currently represented on front-end form
                .ForMember(dest => dest.ReturnName1, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnName1a, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnAddress1, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnAddress1a, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnPhone1, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnName2, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnName2a, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnAddress2, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnAddress2a, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnPhone2, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnName3, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnName3a, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnAddress3, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnAddress3a, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.ReturnPhone3, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.Date3, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.D1, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.D2, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                .ForMember(dest => dest.D3, opt => opt.MapFrom(src => "")) // NOTE: not currently represented on front-end form
                ;

            // TODO: CreateMap for SummonsForUnlawfulDetainer
            #endregion

            // TODO: Figure out what exactly this does 
            //CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
        }
    }
}
