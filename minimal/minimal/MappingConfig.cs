using AutoMapper;
using minimal.DTO;
using minimal.Models;

namespace minimal
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<LocalUser, LoginRequestDTO>().ReverseMap();
            CreateMap<LocalUser, LoginResponseDTO>().ReverseMap();
            CreateMap<LocalUser, RegistrationRequestDTO>().ReverseMap();
            CreateMap<LocalUser, UserDTO>().ReverseMap();
        }
    }
}
