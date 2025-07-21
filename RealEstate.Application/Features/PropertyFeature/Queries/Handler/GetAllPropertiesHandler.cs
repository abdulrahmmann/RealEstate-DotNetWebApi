using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetAllPropertiesHandler(IUnitOfWork unitOfWork, ILogger<Property> logger)
    : IRequestHandler<GetAllPropertiesRequest, BaseResponse<IEnumerable<PropertyDto>>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion
    
    public async Task<BaseResponse<IEnumerable<PropertyDto>>> Handle(GetAllPropertiesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Get All Properties.
            var properties = await _unitOfWork.GetPropertyRepository.GetAllPropertiesAsync();
            
            // 2. Check Properties Null.
            if (properties == null) return BaseResponse<IEnumerable<PropertyDto>>.NotFound();
            
            // 3. Check Properties Empty.
            var enumerable = properties.ToList();
            if (!enumerable.Any()) return BaseResponse<IEnumerable<PropertyDto>>.NoContent();

            // 4. Mapping.
            var mapped = enumerable.To_PropertyDto_List();
            
            // 5. Get Properties Count.
            var counts = enumerable.Count();
            
            // 6. Return Properties.
            return BaseResponse<IEnumerable<PropertyDto>>.Success(mapped, "Properties Retrieved Successfully", counts);

        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<IEnumerable<PropertyDto>>.InternalError(e.Message);
        }
    }
}