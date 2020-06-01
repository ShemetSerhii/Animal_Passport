using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Utils;
using AnimalPassport.Entities.Entities;
using AutoMapper;

namespace AnimalPassport.BusinessLogic.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(x => x.Password, opt => opt.MapFrom(src => CryptoProvider.HashPassword(src.Password)))
                .ForMember(x => x.Role, opt => opt.Ignore());

            CreateMap<User, UserModel>()
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<User, UserInfo>();

            CreateMap<Role, RoleDto>();
        }
    }
}