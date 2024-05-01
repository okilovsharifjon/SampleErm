using Erm.BusinessLayer;

using Microsoft.AspNetCore.Mvc;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Erm.PresentationLayer.WebApi.Controllers
{
    [ApiController]
    [Route("riskprofiles")]
    public class RiskProfileController(
        IRiskProfileService profileService): ControllerBase
    {
        private readonly IRiskProfileService _riskProfileService = profileService;


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RiskProfileDto profileDto)
        {
            int id = await _riskProfileService.CreateAsync(profileDto);
            return Ok(id);

        }

        [HttpGet]
        public async Task<IActionResult> Query([FromQuery] string query)
            => Ok(await _riskProfileService.QueryAsync(query));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
            => Ok(await _riskProfileService.GetAsync(id));

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _riskProfileService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody]RiskProfileDto profileDto)
        {
            await _riskProfileService.UpdateAsync(id, profileDto);
            return Ok();

        }

    }
}
