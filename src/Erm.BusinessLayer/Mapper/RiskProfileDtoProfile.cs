using AutoMapper;

using Erm.DataAccess;

namespace Erm.BusinessLayer;

public sealed class RiskProfileDtoProfile : Profile
{
    public RiskProfileDtoProfile()
    {
        CreateMap<RiskProfileDto, RiskProfile>()
            .ForMember(dest => dest.RiskName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BusinessProcessId, opt => opt.MapFrom(src => src.BusinessProcessId))
            .ForMember(dest => dest.OccurrenceProbability, opt => opt.MapFrom(src => src.OccurrenceProbability))
            .ForMember(dest => dest.PotentialBusinessImpact, opt => opt.MapFrom(src => src.PotentialBusinessImpact))
            .ForMember(dest => dest.PotentialSolution, opt => opt.MapFrom(src => src.PotentialSolution));
            
    }
}