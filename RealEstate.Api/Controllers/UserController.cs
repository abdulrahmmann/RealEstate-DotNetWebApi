using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.UserFeature.Commands;
using RealEstate.Application.Features.UserFeature.DTOs;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new RegisterUserCommand(userDto));

            return Ok(result);
        }
        
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new LoginUserCommand(userDto));

            return Ok(result);
        }
    }
}
