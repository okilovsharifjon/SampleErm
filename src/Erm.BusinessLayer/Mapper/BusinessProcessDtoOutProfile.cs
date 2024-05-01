

using AutoMapper;

using Erm.DataAccess;

namespace Erm.BusinessLayer.Mapper;

public sealed class BusinessProcessDtoOutProfile : Profile
{
    public BusinessProcessDtoOutProfile()
    {
        CreateMap<BusinessProcess, BusinessProcessDtoOut>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Domain, opt => opt.MapFrom(src => src.Domain));

    }
}
