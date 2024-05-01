namespace Erm.DataAccess;

public interface IRiskProfileRepository
{
    public Task<int> CreateAsync(RiskProfile entity, CancellationToken token = default);
    public Task<RiskProfile> GetAsync(int id, CancellationToken token = default);
    public Task UpdateAsync(int id, RiskProfile riskProfile, CancellationToken token = default);
    public Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
}
