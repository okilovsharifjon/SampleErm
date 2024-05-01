using AutoMapper;
using Erm.DataAccess;
using Erm.DataAccess.Repositories;

using FluentValidation;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Erm.BusinessLayer.Services;

public class BusinessProcessService(
    BusinessProcessRepository processRepository,
    IMapper mapper) : IBusinessProcessService
{
    private readonly IMapper _mapper = mapper;
    private readonly BusinessProcessRepository _processRepository = processRepository;

    public async Task CreateAsync(BusinessProcessDto processDto, CancellationToken token = default)
    {
        BusinessProcess businessProcess = _mapper.Map<BusinessProcess>(processDto);
        await _processRepository.CreateAsync(businessProcess, token);
    }
    public async Task<IEnumerable<BusinessProcessDtoOut>> QueryAsync(string query, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(query);

        return _mapper.Map<IEnumerable<BusinessProcessDtoOut>>(await _processRepository.QueryAsync(query, token));
    }
    public async Task UpdateAsync(int id, BusinessProcessDto processDto, CancellationToken token = default)
    {
        BusinessProcess businessProcess = _mapper.Map<BusinessProcess>(processDto);
        await _processRepository.UpdateAsync(id, businessProcess, token);
    }
    public async Task DeleteAsync(int id, CancellationToken token = default)
        => await _processRepository.DeleteAsync(id, token);
    public async Task<BusinessProcessDto> GetAsync(int id, CancellationToken token = default)
    {
        BusinessProcess businessProcess = await _processRepository.GetAsync(id, token);
        BusinessProcessDto processDto = _mapper.Map<BusinessProcessDto>(businessProcess);
        return processDto;
    }
}
