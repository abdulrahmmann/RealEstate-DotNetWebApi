using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : AppControllerBase
    {
        #region GET

        [HttpGet]
        [Route("properties")]
        public async Task<IActionResult> GetAllProperties()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAllPropertiesRequest());

            return NewResult(result);
        }
        #endregion
        
        
        #region POST 
        [HttpPost]
        [Route("add-property")]
        public async Task<IActionResult> AddProperty([FromBody] AddPropertyDto propertyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddPropertyRequest(propertyDto));

            return NewResult(result);
        }
        #endregion
        
        
        #region PUT 
        #endregion
        
        
        #region DELETE 
        #endregion
    }
}
