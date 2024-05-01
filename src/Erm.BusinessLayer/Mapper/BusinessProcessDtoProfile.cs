

using AutoMapper;

using Erm.DataAccess;

namespace Erm.BusinessLayer.Mapper;

public sealed class BusinessProcessDtoProfile : Profile
{
    public BusinessProcessDtoProfile()
    {
        CreateMap<BusinessProcessDto, BusinessProcess>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain));

    }
}
