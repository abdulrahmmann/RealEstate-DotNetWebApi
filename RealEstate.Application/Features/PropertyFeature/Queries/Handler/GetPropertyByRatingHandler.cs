using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyByRatingHandler(IUnitOfWork unitOfWork, ILogger<Property> logger):
    IRequestHandler<GetPropertyByRatingRequest, BaseResponse<IQueryable<PropertyDto>>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion

    public Task<BaseResponse<IQueryable<PropertyDto>>> Handle(GetPropertyByRatingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Request Raring Is Valid.
            if (request.Rating < 0 || request.Rating > 5) 
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.ValidationError("rating < 0 || rating > 5"));
            
            // 2. Get Properties Per Raring.
            var properties = _unitOfWork.GetPropertyRepository.SearchPropertyByRating(request.Rating);
            
            // 3. Check Properties Empty.
            if (!properties.Any())
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.NoContent("No Content properties found"));

            // 4. Map
            var mapped = properties.To_PropertyDto_Queryable();
            
            // 5. Return Properties.
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.Success(mapped));
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.InternalError(e.Message));
        }
    }
}