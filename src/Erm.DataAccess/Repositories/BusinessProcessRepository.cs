using Microsoft.EntityFrameworkCore;

namespace Erm.DataAccess.Repositories;

public sealed class BusinessProcessRepository(ErmDbContext ermDbContext) : IBusinessProcessRepository
{
    private readonly ErmDbContext _db = ermDbContext;

    public async Task CreateAsync(BusinessProcess businessProcess, CancellationToken token = default)
    {
        await _db.BusinessProcesses.AddAsync(businessProcess, token);
        await _db.SaveChangesAsync(token);
    }
    public async Task<IEnumerable<BusinessProcess>> QueryAsync(string query, CancellationToken token = default)
        => await _db.BusinessProcesses.AsNoTracking().Where(x => x.Name.Contains(query) || x.Domain.Contains(query)).ToArrayAsync(token);
    public async Task UpdateAsync(int id, BusinessProcess businessProcess, CancellationToken token = default)
    {
        BusinessProcess processToUpdate = await _db.BusinessProcesses.SingleAsync(x => x.Id.Equals(id), token);
        processToUpdate.Name = businessProcess.Name;
        processToUpdate.Domain = businessProcess.Domain;
        await _db.SaveChangesAsync(token);
    }
    public async Task DeleteAsync(int id, CancellationToken token = default)
    {
        await _db.BusinessProcesses.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync(token);
        await _db.SaveChangesAsync(token);
    }
    public async Task<BusinessProcess> GetAsync(int id, CancellationToken token = default)
        => await _db.BusinessProcesses.SingleAsync(x => x.Id.Equals(id), token);
 }
