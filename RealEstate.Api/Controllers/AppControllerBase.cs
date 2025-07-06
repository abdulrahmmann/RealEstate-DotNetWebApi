using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Common;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        #region Mediator Instance
        
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
        
        #endregion
        
        
        #region Standardized Response Handler
        protected ObjectResult NewResult<T>(BaseResponse<T> response)
        {
            return response.HttpStatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.Created => Created(string.Empty, response),
                HttpStatusCode.Unauthorized => Unauthorized(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Accepted => Accepted(response),
                HttpStatusCode.UnprocessableEntity => UnprocessableEntity(response),
                HttpStatusCode.Conflict => Conflict(response),
                _ => StatusCode((int)response.HttpStatusCode, response)
            };
        }
        #endregion
    }
}
