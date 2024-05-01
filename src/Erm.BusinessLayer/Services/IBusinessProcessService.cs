
namespace Erm.BusinessLayer.Services;

public interface IBusinessProcessService
{
    public Task CreateAsync(BusinessProcessDto processDto, CancellationToken token = default);
    public Task<IEnumerable<BusinessProcessDtoOut>> QueryAsync(string query, CancellationToken token = default);
    public Task UpdateAsync(int id, BusinessProcessDto processDto, CancellationToken token = default);
    public Task DeleteAsync(int id, CancellationToken token = default);
    public Task<BusinessProcessDto> GetAsync(int id, CancellationToken token = default);
}
