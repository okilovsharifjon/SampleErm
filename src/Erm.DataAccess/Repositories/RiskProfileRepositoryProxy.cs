using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;

using StackExchange.Redis;

namespace Erm.DataAccess;

public sealed class RiskProfileRepositoryProxy(
    IDistributedCache distributedCache,
    RiskProfileRepository originalRepository) : IRiskProfileRepository
{
    private readonly IDistributedCache _db = distributedCache;

    private readonly RiskProfileRepository _originalRepository = originalRepository;

    public async Task<int> CreateAsync(RiskProfile entity, CancellationToken token = default) 
    {
        int id = await _originalRepository.CreateAsync(entity, token);
        return id;
    }

    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        await _originalRepository.DeleteAsync(id, token);
        await _db.RemoveAsync(id.ToString(), token);
    }

    public async Task<RiskProfile> GetAsync(int id, CancellationToken token = default)
    {
        string idstr = id.ToString();
        string? redisValue = await _db.GetStringAsync(idstr, token);
        if (string.IsNullOrEmpty(redisValue))
        {
            RiskProfile riskProfileFromDB = await _originalRepository.GetAsync(id, token);
            string redisProfileJson = JsonSerializer.Serialize(riskProfileFromDB);
            
            await _db.SetStringAsync(idstr, redisProfileJson, token);
            return riskProfileFromDB;

        }

        string redisProfileJsonStr = redisValue.ToString();
    
        RiskProfile riskProfile = JsonSerializer.Deserialize<RiskProfile>(redisProfileJsonStr)
             ?? throw new InvalidOperationException();
        return riskProfile;
            
    }

    public async Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default) 
        => await _originalRepository.QueryAsync(query, token);

    public async Task UpdateAsync(int id, RiskProfile riskProfile, CancellationToken token = default)
    {
        string idstr = id.ToString();
        await _originalRepository.UpdateAsync(id, riskProfile, token);
        await _db.RemoveAsync(idstr, token);
        RiskProfile riskProfileFromDB = await _originalRepository.GetAsync(id, token);
        string redisProfileJson = JsonSerializer.Serialize(riskProfileFromDB);

        await _db.SetStringAsync(idstr, redisProfileJson, token);
    }
}
