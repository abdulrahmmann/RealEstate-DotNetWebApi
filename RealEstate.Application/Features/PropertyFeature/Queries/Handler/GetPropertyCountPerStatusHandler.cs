using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetPropertyCountPerStatusHandler(IUnitOfWork unitOfWork, ILogger<Property> logger): 
    IRequestHandler<GetPropertyCountPerStatusRequest, BaseResponse<Dictionary<string, int>>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion

    public async Task<BaseResponse<Dictionary<string, int>>> Handle(GetPropertyCountPerStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var grouped = await _unitOfWork.GetPropertyRepository.GetPropertyCountPerStatusAsync();
            
            if (grouped == null) return BaseResponse<Dictionary<string, int>>.BadRequest();
            
            return BaseResponse<Dictionary<string, int>>.Success(grouped);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Dictionary<string, int>>.InternalError(e.Message);
        }
    }
}