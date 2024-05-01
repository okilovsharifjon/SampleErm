using AutoMapper;
using Erm.DataAccess;

namespace Erm.BusinessLayer;

internal static class AutoMapperHelper
{
    internal readonly static MapperConfiguration MapperConfiguration = new(opt =>
    {
        opt.AddProfile<RiskProfileDtoProfile>();
    });
}
