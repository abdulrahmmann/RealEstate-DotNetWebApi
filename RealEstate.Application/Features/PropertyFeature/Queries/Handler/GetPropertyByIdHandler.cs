using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyByIdHandler(IUnitOfWork unitOfWork, ILogger<Property> logger)
    : IRequestHandler<GetPropertyByIdRequest, BaseResponse<PropertyDto>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion
    
    public async Task<BaseResponse<PropertyDto>> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Request Id Validation.
            if (request.Id <= 0)
            {
                return BaseResponse<PropertyDto>.ValidationError("Request can not be less than or equal to zero");
            }
            
            // 2. Get Property By Id
            var property = await _unitOfWork.GetRepository<Property>().GetByIdAsync(request.Id);
            
            // 3. Check Property NUllS.
            if (property is null)
            {
                return BaseResponse<PropertyDto>.NotFound("Property not found");
            }
            
            // 4. Mapping.
            var mapped = property.To_PropertyDto();
            
            // 5. Return Property.
            return BaseResponse<PropertyDto>.Success(mapped);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<PropertyDto>.InternalError(e.Message);
        }
    }
}