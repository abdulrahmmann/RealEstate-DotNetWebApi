using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : AppControllerBase
    {
        #region GET
        [HttpGet()]
        [Route("agencies")]
        public async Task<IActionResult> GetAllAgencies()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAllAgenciesRequest());

            return NewResult(result);
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<IActionResult> GetAgencyById([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAgencyByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("search-name/{name}")]
        public async Task<IActionResult> SearchAgenciesByName([FromQuery] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new SearchAgencyByNameRequest(name));

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("tax/{taxNumber}")]
        public async Task<IActionResult> SearchAgenciesByTaxNumber([FromQuery] string taxNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAgencyByTaxNumberRequest(taxNumber));

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("license/{licenseNumber}")]
        public async Task<IActionResult> SearchAgenciesByLicenseNumber([FromQuery] string licenseNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetAgencyByLicenseNumberRequest(licenseNumber));

            return NewResult(result);
        }
        #endregion
        
        
        #region POST
        [HttpPost]
        [Route("add-agency")]
        public async Task<IActionResult> AddAgency([FromBody] AddAgencyDto agencyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddAgencyRequest(agencyDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("add-agencies")]
        public async Task<IActionResult> AddAgencies([FromBody] IEnumerable<AddAgencyDto> agenciesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddAgencyRangeRequest(agenciesDto));
            
            return NewResult(result);
        }
        #endregion
        

        #region DELETE
        [HttpDelete]
        [Route("delete-agency/{id}")]
        public async Task<IActionResult> DeleteAgency([FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new DeleteAgencyRequest(id));

            return NewResult(result);
        }
        #endregion
    }
}
