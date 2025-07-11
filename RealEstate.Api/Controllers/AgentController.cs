using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.AgentFeature.Commands;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Application.Features.AgentFeature.Queries;

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
        
        [HttpGet()]
        [Route("agents")]
        public async Task<IActionResult> GetAgents()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new GetAllAgentsQuery());

            return Ok(result);
        }
        
        [HttpGet()]
        [Route("id")]
        public async Task<IActionResult> GetAgentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new GetAgentByIdQuery(id));

            return Ok(result);
        }
        
        [HttpGet()]
        [Route("name")]
        public async Task<IActionResult> GetAgentByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new GetAgentsByNameQuery(name));

            return Ok(result);
        }
        
        [HttpGet()]
        [Route("email")]
        public async Task<IActionResult> GetAgentByEmail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new GetAgentByEmailQuery(email));

            return Ok(result);
        }
        
        [HttpGet()]
        [Route("phoneNumber")]
        public async Task<IActionResult> GetAgentByPhoneNumber(string phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new GetAgentByPhoneQuery(phoneNumber));

            return Ok(result);
        }
    }
}
