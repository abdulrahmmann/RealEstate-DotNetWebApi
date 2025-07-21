using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyByNameHandler(IUnitOfWork unitOfWork, ILogger<Property> logger)
    : IRequestHandler<GetPropertyByNameRequest, BaseResponse<PropertyDto>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion

    
    public async Task<BaseResponse<PropertyDto>> Handle(GetPropertyByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Validate request name.
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BaseResponse<PropertyDto>.ValidationError("Request can not be null or empty");
            }
            
            // 2. Get Property By Name.
            var property = await _unitOfWork.GetPropertyRepository.GetPropertyByNameAsync(request.Name);
            
            // 3. Check Property NUllS.
            if (property == null)
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