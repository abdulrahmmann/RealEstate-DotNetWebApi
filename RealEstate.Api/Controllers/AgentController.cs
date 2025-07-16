using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.AgentFeature.Commands.Requests;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Application.Features.AgentFeature.Queries.Requests;

namespace RealEstate.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class AgentController : AppControllerBase
    {
        #region GET
        [HttpGet()]
        [Route("agents")]
        public async Task<IActionResult> GetAllAgents()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAllAgentsRequest());

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("name")]
        public async Task<IActionResult> GetAgentsByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new SearchAgentByNameRequest(name));

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("email")]
        public async Task<IActionResult> GetAgentByEmail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAgentByEmailRequest(email));

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("phone")]
        public async Task<IActionResult> GetAgentByPhone(string phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAgentByPhoneRequest(phone));

            return NewResult(result);
        }
        #endregion
        
        
        #region POST
        [HttpPost]
        [Route("add-agent")]
        public async Task<IActionResult> AddAgent([FromBody] AddAgentDto agentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddAgentRequest(agentDto));

            return NewResult(result);
        }
        
        
        [HttpPost]
        [Route("add-agent-range")]
        public async Task<IActionResult> AddAgentsRange([FromBody] IEnumerable<AddAgentDto> agentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddAgentRangeRequest(agentDto));

            return NewResult(result);
        }
        #endregion
        
        
    }
}
