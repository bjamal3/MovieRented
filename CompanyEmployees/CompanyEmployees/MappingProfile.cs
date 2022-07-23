using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.ViewModels;

namespace CompanyEmployees
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ReverseMap();

            CreateMap<Movie, MovieViewModel>()
                .ReverseMap();
            

            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
                .ReverseMap();
        }
    }
}
