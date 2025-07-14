using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class DeleteAgencyHandler: IRequestHandler<DeleteAgencyRequest, BaseResponse<int>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork;
    #endregion
    
    #region CONSTRUCTOR
    public DeleteAgencyHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<int>> Handle(DeleteAgencyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<int>.ValidationError("request Id cannot be less or equal than 0");
            }

            var isExist = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);

            if (isExist is null)
            {
                return BaseResponse<int>.NotFound($"Agency with Id: {request.Id} does not found");
            }

            _unitOfWork.GetRepository<Agency>().Delete(request.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<int>.Success($"Agency with Id: {request.Id} deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<int>.InternalError(e.Message);
        }
    }
}