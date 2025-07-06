using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.AgencyFeature.Commands;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : AppControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> AddAgency([FromBody] AgencyDto agencyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new AddAgencyCommand(agencyDto));

            return Ok(result);
        }
    }
}
