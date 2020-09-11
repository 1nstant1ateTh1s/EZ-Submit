using AutoMapper;
using DocxConverterService.Models;
using EZSubmitApp.Core.Mapper.Converters;
using EZSubmitApp.Core.Mapper.Resolvers;

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
            // TODO 
            CreateMap<Entities.WarrantInDebtForm, WarrantInDebtDocxFormFields>()
                //.ForMember(dest => dest.Court, opt => opt.MapFrom("CHESAPEAKE")) // TODO: READ FROM CONFIGURATION
                //.ForMember(dest => dest.CourtAddress, opt => opt.MapFrom("307 Albemarle Drive, Suite 200B, Chesapeake, VA 23322, PH-757-382-3143 FAX-757-382-3113")) // TODO: READ FROM CONFIGURATION
                .ForMember(dest => dest.HearingDate, opt => opt.MapFrom(src => src.HearingDateTime.ToString("MM/dd/yyyy")))
                .ForMember(dest => dest.HearingTime, opt => opt.MapFrom(src => src.HearingDateTime.ToString("hh:mm tt")))
                .ForMember(dest => dest.A1, opt => opt.MapFrom<A1TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.A2, opt => opt.MapFrom<A2TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.A3, opt => opt.MapFrom<A3TypeResolver, string>(src => src.AccountType))
                .ForMember(dest => dest.A4, opt => opt.MapFrom<A4TypeResolver, string>(src => src.AccountType));
                // TODO ...

            // TODO: CreateMap for SummonsForUnlawfulDetainer
            #endregion

            // TODO: Figure out what exactly this does 
            //CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
        }
    }
}
