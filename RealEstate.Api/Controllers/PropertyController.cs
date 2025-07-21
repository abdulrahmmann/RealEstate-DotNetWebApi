using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Enums;

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
        
        [HttpGet]
        [Route("name={name}")]
        public async Task<IActionResult> GetPropertyByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByNameRequest(name));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("list/name={name}")]
        public async Task<IActionResult> SearchPropertyByName(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new SearchPropertyByNameRequest(name));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("id={id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByIdRequest(id));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("country={country}")]
        public async Task<IActionResult> GetPropertyByCountry(string country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByCountryRequest(country));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("city={city}")]
        public async Task<IActionResult> GetPropertyByCity(string city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByCityRequest(city));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("type={type}")]
        public async Task<IActionResult> GetPropertyByType([FromQuery] string type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByTypeRequest(type));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("status={status}")]
        public async Task<IActionResult> GetPropertyByStatus([FromQuery] string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByStatusRequest(status));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("rating={rating}")]
        public async Task<IActionResult> GetPropertyByRating(int rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByRatingRequest(rating));

            return NewResult(result);
        }
        
        [HttpGet]
        [Route("price={price1}/{price2}")]
        public async Task<IActionResult> GetPropertyByPrice(decimal price1, decimal price2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByPriceRequest(price1, price2));

            return NewResult(result);
        }
        
        [HttpGet()]
        [Route("date={listedDate}")]
        public async Task<IActionResult> GetPropertyByListedDate([FromQuery] DateOnly listedDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetPropertyByListedDateRequest(listedDate));

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
