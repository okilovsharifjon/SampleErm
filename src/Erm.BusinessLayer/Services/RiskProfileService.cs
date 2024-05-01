using System.Diagnostics;

using AutoMapper;


using Erm.DataAccess;

using FluentValidation;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Erm.BusinessLayer;

public sealed class RiskProfileService(
    RiskProfileRepositoryProxy riskProfile,
    IMapper mapper,
    IValidator<RiskProfileDto> validator): IRiskProfileService
{
    private readonly RiskProfileRepositoryProxy _repository = riskProfile;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<RiskProfileDto> _validationRules = validator;


    public async Task<int> CreateAsync(RiskProfileDto profileInfo, CancellationToken token = default)
    {
        await _validationRules.ValidateAndThrowAsync(profileInfo, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(profileInfo);
        int id = await _repository.CreateAsync(riskProfile, token);
        return id;
    }

    public async Task UpdateAsync(int id, RiskProfileDto profileInfo, CancellationToken token = default)
    {
        await _validationRules.ValidateAndThrowAsync(profileInfo, token);

        RiskProfile riskProfile = _mapper.Map<RiskProfile>(profileInfo);
        await _repository.UpdateAsync(id, riskProfile, token);
    }
    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        await _repository.DeleteAsync(id, token);
    }

    public async Task<IEnumerable<RiskProfileDtoOut>> QueryAsync(string query, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);

        return _mapper.Map<IEnumerable<RiskProfileDtoOut>>(await _repository.QueryAsync(query, token));
    }

    public async Task<RiskProfileDtoOut> GetAsync(int id, CancellationToken token = default)
    {
        
        RiskProfile riskProfile = await _repository.GetAsync(id, token);
        RiskProfileDtoOut profileInfo = _mapper.Map<RiskProfileDtoOut>(riskProfile);
        return profileInfo;
    }
}