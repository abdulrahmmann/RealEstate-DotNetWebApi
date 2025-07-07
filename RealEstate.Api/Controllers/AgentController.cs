using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.AgentFeature.Commands;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : AppControllerBase
    {
        [HttpPost()]
        [Route("create-agent")]
        public async Task<IActionResult> CreateAgent(AddAgentDto agentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new AddAgentCommand(agentDto));

            return Ok(result);
        }
    }
}
