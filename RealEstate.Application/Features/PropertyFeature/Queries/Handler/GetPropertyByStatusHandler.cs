using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyByStatusHandler(IUnitOfWork unitOfWork, ILogger<Property> logger): 
    IRequestHandler<GetPropertyByStatusRequest, BaseResponse<IQueryable<PropertyDto>>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion

    public Task<BaseResponse<IQueryable<PropertyDto>>> Handle(GetPropertyByStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Request Status Is Valid.
            if (string.IsNullOrWhiteSpace(request.Status)) 
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.ValidationError("Request Status Is Null Or Empty"));
            
            // 2. Check Request Status is Exist on PropertyStatus Enum.
            if (!Enum.TryParse<PropertyStatus>(request.Status, true, out var status))
            {
                return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.ValidationError("Invalid status value."));
            }

            // 3. Get Properties.
            var properties = _unitOfWork.GetPropertyRepository.SearchPropertyByStatus(status);
            
            // 4. Check Properties
            if (!properties.Any()) return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.NoContent("No Content Properties Found"));

            // 5. Mapping.
            var mapped = properties.To_PropertyDto_Queryable();
            
            // 6. Return Properties.
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.Success(mapped));
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return Task.FromResult(BaseResponse<IQueryable<PropertyDto>>.InternalError(e.Message));
        }
    }
}