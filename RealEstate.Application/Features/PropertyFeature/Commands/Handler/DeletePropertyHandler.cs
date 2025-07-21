using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Handler;

public class DeletePropertyHandler(IUnitOfWork unitOfWork, ILogger<Property> logger):
    IRequestHandler<DeletePropertyRequest, BaseResponse<Unit>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id < 0) return BaseResponse<Unit>.ValidationError("id < 0");

            var property = await _unitOfWork.GetRepository<Property>().GetByIdAsync(request.Id);
            
            if (property == null) return BaseResponse<Unit>.NotFound("property not found");

            property.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Property with id {request.Id} deleted successfully");

        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}