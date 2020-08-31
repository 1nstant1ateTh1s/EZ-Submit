using AutoMapper;
using EZSubmitApp.Core.Mapper.Profiles;
using System;

namespace EZSubmitApp.Core.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                // Map profiles
                // TODO: Can this be simplified to just iterate over the current AppDomain to retrieve all Profile artifacts?
                cfg.AddProfile<ApplicationUsersProfile>();
                cfg.AddProfile<CaseFormsProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
