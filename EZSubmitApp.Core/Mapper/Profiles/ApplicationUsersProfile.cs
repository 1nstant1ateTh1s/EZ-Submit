using AutoMapper;

namespace EZSubmitApp.Core.Mapper.Profiles
{
    public class ApplicationUsersProfile : Profile
    {
        public ApplicationUsersProfile()
        {
            CreateMap<Entities.ApplicationUser, DTOs.ApplicationUserDto>();

            // TODO: Create mapping for ApplicationUserForCreationDto-to-entity, if needed
        }
    }
}
