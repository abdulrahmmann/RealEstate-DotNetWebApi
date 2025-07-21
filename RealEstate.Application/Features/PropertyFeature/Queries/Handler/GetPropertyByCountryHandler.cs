using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyByCountryHandler(IUnitOfWork unitOfWork, ILogger<Property> logger): 
    IRequestHandler<GetPropertyByCountryRequest, BaseResponse<IQueryable<PropertyDto>>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion
    
    public Task<BaseResponse<IQueryable<PropertyDto>>> Handle(GetPropertyByCountryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Request Country Validation.
            if (string.IsNullOrEmpty(request.Country))
            {
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.ValidationError("Request can not be null or empty"));
            }
            
            // 2. Get Property By Country
            var properties = _unitOfWork.GetPropertyRepository.SearchPropertyByCountry(request.Country);
            
            // 3. Check Property NUllS.
            if (properties is null)
            {
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.NotFound("Property not found"));
            }
            
            // 4. Mapping.
            var mapped = properties.AsEnumerable().To_PropertyDto_Queryable();
            
            // 5. Return Property.
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.Success(mapped, $"Properties By County: {request.Country} Retrieved Successfully", properties.Count()));
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.InternalError(e.Message));
        }
    }
}