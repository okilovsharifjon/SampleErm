using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Erm.BusinessLayer;

namespace Erm.PresentationLayer.Console
{
    internal class RiskProfileController(IRiskProfileService profileService)
    {
        private readonly IRiskProfileService _riskProfileService = profileService;

        public async Task CreateAsync(RiskProfileDto profileInfo)
        {
            await _riskProfileService.CreateAsync(profileInfo);
        }

        public async Task UpdateAsync(int id, RiskProfileDto profileInfo)
        {
            await _riskProfileService.UpdateAsync(id, profileInfo);
        }

        public async Task DeleteAsync(int id)
        {
            await _riskProfileService.DeleteAsync(id);
        }

        public async Task<IEnumerable<RiskProfileDto>> QueryAsync(string query)
        {
            return await _riskProfileService.QueryAsync(query);
        }


    }


}
