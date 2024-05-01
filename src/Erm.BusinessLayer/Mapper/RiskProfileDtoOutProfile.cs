using AutoMapper;

using Erm.DataAccess;

namespace Erm.BusinessLayer;

public sealed class RiskProfileDtoOutProfile : Profile
{
    public RiskProfileDtoOutProfile()
    {
        CreateMap<RiskProfile, RiskProfileDtoOut>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RiskName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.BusinessProcessId, opt => opt.MapFrom(src => src.BusinessProcessId))
            .ForMember(dest => dest.OccurrenceProbability, opt => opt.MapFrom(src => src.OccurrenceProbability))
            .ForMember(dest => dest.PotentialBusinessImpact, opt => opt.MapFrom(src => src.PotentialBusinessImpact))
            .ForMember(dest => dest.PotentialSolution, opt => opt.MapFrom(src => src.PotentialSolution))
            .ForMember(dest => dest.RiskLevel, opt => opt.MapFrom(src => src.RiskLevel));
    }
}