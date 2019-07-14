using System;
using AutoMapper;
using SampleAppWithDapper.Domain.DomainModels.Identity;
using SampleAppWithDapper.Domain.Models;

namespace SampleAppWithDapper.Domain.MappingConfigurationFiles
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
          
            CreateMap<UserViewModel, AppUser>()
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => string.Join(";", src.Roles)));
            CreateMap<AppUser, UserViewModel>()
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles.Split(Convert.ToChar(";"))));


            CreateMissingTypeMaps = true;
        }

    }
}
