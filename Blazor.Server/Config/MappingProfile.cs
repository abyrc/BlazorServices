using BlazorCRUD.Shared;
using AutoMapper;

namespace Blazor.Server.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, RegisterDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>()
           .ForMember(dest => dest.nombreCompleto, opt => opt
               .MapFrom(src => src.Nombre + " " + src.ApellidoPaterno + " " + src.ApellidoMaterno));
        }
    }
}
