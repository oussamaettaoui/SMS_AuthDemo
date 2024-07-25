using AutoMapper;
using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Application.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
