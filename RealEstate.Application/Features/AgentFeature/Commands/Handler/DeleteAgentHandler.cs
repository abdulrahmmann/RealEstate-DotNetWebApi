using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Commands.Handler;

public class DeleteAgentHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteAgentRequest, BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(DeleteAgentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("request Id cannot be less or equal than 0");
            }

            var isExist = await _unitOfWork.GetRepository<Agent>().GetByIdAsync(request.Id);
            
            if (isExist is null)
            {
                return BaseResponse<Unit>.NotFound($"Agent with Id: {request.Id} does not found");
            }
            
            _unitOfWork.GetRepository<Agent>().Delete(request.Id);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Agent with Id: {request.Id} deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}