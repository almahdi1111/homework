

using AutoMapper;
using homeworksatarday.Models.Entites;
using homeworksatarday.Models.RequestDTO;
using homeworksatarday.Models.Rresponse;

namespace homeworksatarday.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserAddDTO, Users>().ForMember(ExptAttribut => ExptAttribut.IsDeleted, opt => opt.MapFrom(value => false));
            CreateMap<UserUpdateDTO, Users>();
            CreateMap<Users, UserResponseDTO>();
        }
    }
}
