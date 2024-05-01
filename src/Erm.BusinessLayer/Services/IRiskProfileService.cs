using Erm.DataAccess;
namespace Erm.BusinessLayer;


public interface IRiskProfileService
{
    public Task<int> CreateAsync(RiskProfileDto profileInfo, CancellationToken token = default);
    public Task<IEnumerable<RiskProfileDtoOut>> QueryAsync(string query, CancellationToken token = default);
    public Task UpdateAsync(int id, RiskProfileDto profileInfo, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
    public Task<RiskProfileDtoOut> GetAsync(int id, CancellationToken token = default);
}