using Microsoft.EntityFrameworkCore;

namespace Erm.DataAccess;

public sealed class RiskProfileRepository(ErmDbContext dbContext) : IRiskProfileRepository
{
    private readonly ErmDbContext _db = dbContext;

    public async Task<int> CreateAsync(RiskProfile entity, CancellationToken token = default)
    {
        await _db.RiskProfiles.AddAsync(entity, token);
        await _db.SaveChangesAsync(token);
        return entity.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        await _db.RiskProfiles.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync(token);
        await _db.SaveChangesAsync(token);
    }

    public async Task<RiskProfile> GetAsync(int id, CancellationToken token = default)
        => await _db.RiskProfiles.SingleAsync(x => x.Id.Equals(id), token);

    public async Task<IEnumerable<RiskProfile>> QueryAsync(string query, CancellationToken token = default)
        => await _db.RiskProfiles.AsNoTracking().Where(x => x.RiskName.Contains(query) || x.Description.Contains(query)).ToArrayAsync(token);

    public async Task UpdateAsync(int id, RiskProfile riskProfile, CancellationToken token = default)
    {
        RiskProfile profileToUpdate = await _db.RiskProfiles.SingleAsync(x => x.Id.Equals(id), token);

        profileToUpdate.RiskName = riskProfile.RiskName;
        profileToUpdate.Description = riskProfile.Description;
        profileToUpdate.PotentialBusinessImpact = riskProfile.PotentialBusinessImpact;
        profileToUpdate.PotentialSolution = riskProfile.PotentialSolution;
        profileToUpdate.OccurrenceProbability = riskProfile.OccurrenceProbability;

        await _db.SaveChangesAsync(token);
    }
}

