using Microsoft.AspNetCore.Mvc;
using Erm.BusinessLayer;
using Erm.BusinessLayer.Services;

namespace Erm.PresentationLayer.WebApi.Controllers
{
    [ApiController]
    [Route("businessprocess")]
    public class BusinessProcessController(
        IBusinessProcessService processService): ControllerBase
    {
        private readonly IBusinessProcessService  _processService = processService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]BusinessProcessDto processDto)
        {
            await _processService.CreateAsync(processDto);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
            => Ok(await _processService.GetAsync(id));

        [HttpGet]
        public async Task<IActionResult> Query([FromQuery] string query)
            => Ok(await _processService.QueryAsync(query));

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BusinessProcessDto processDto)
        {
            await _processService.UpdateAsync(id, processDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _processService.DeleteAsync(id);
            return Ok();
        }
         
    }
}
