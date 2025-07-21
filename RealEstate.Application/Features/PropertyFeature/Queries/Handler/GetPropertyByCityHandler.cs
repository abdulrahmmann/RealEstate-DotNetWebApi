using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyByCityHandler(IUnitOfWork unitOfWork, ILogger<Property> logger): 
    IRequestHandler<GetPropertyByCityRequest, BaseResponse<IQueryable<PropertyDto>>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion

    public Task<BaseResponse<IQueryable<PropertyDto>>> Handle(GetPropertyByCityRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Request City Validation.
            if (string.IsNullOrEmpty(request.City))
            {
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.ValidationError("Request can not be null or empty"));
            }
            
            // 2. Get Property By City
            var properties = _unitOfWork.GetPropertyRepository.SearchPropertyByCountry(request.City);
            
            // 3. Check Property NUllS.
            if (properties is null)
            {
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.NotFound("Property not found"));
            }
            
            // 4. Mapping.
            var mapped = properties.To_PropertyDto_Queryable();
            
            // 5. Return Property.
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.Success(mapped, $"Properties By City: {request.City} Retrieved Successfully", properties.Count()));
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.InternalError(e.Message));
        }
    }
}