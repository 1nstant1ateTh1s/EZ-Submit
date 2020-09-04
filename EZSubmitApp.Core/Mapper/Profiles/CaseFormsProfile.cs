using AutoMapper;
using EZSubmitApp.Core.Mapper.Converters;

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

            // TODO: Figure out what exactly this does 
            //CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
        }
    }
}
