using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class DeleteAgencyHandler(IUnitOfWork unitOfWork,  ILogger<Agency> logger): IRequestHandler<DeleteAgencyRequest, BaseResponse<Unit>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Agency> _logger = logger;
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(DeleteAgencyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The Request ID is Valid.
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("request Id cannot be less or equal than 0");
            }

            // 2. Check If The Agency By ID Is Exist.
            var isExist = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);

            // 3. Check If The Agency Is Null.
            if (isExist is null)
            {
                return BaseResponse<Unit>.NotFound($"Agency with Id: {request.Id} does not found");
            }

            // 4. Delete Agency and SaveChanges.
            _unitOfWork.GetRepository<Agency>().Delete(request.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            // 5. Return.
            return BaseResponse<Unit>.Success($"Agency with Id: {request.Id} deleted successfully");
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}