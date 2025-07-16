using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Application.Features.AgentFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries.Handler;

public class GetAgentByEmailHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAgentByEmailRequest, BaseResponse<AgentDto>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<AgentDto>> Handle(GetAgentByEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BaseResponse<AgentDto>.ValidationError("request can not be null or empty");
            }
            
            var agent = await _unitOfWork.GetAgentRepository.GetAgentByEmail(request.Email);

            if (agent.Equals(null))
            {
                return BaseResponse<AgentDto>.NoContent();
            }
            
            var agentsDto = new AgentDto(
                agent.Name,
                agent.Email,
                agent.Phone,
                agent.ServiceArea,
                agent.ImageUrl,
                agent.Agency!.Name,
                agent.Address.Country,
                agent.Address.City,
                agent.Address.Street!,
                agent.Address.ZipCode!
            );
            
            var agentsCount = await _unitOfWork.GetRepository<Agent>().CountAsync();
            
            return BaseResponse<AgentDto>.Success(agentsDto, $"Agents By Email {request.Email} Retrieved Successful", agentsCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<AgentDto>.InternalError(e.Message);
        }
    }
}