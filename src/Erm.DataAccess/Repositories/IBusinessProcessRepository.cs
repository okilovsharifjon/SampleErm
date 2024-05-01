
namespace Erm.DataAccess.Repositories;

interface IBusinessProcessRepository
{
    public Task CreateAsync(BusinessProcess businessProcess, CancellationToken token = default);
    public Task<IEnumerable<BusinessProcess>> QueryAsync(string query, CancellationToken token = default);
    public Task UpdateAsync(int id, BusinessProcess businessProcess, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
    public Task<BusinessProcess> GetAsync(int id, CancellationToken token = default);
}
