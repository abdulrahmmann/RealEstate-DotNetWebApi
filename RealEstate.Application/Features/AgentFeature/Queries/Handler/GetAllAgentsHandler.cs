using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Application.Features.AgentFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries.Handler;

public class GetAllAgentsHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAllAgentsRequest, BaseResponse<IEnumerable<AgentDto>>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<IEnumerable<AgentDto>>> Handle(GetAllAgentsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var agents = await _unitOfWork.GetRepository<Agent>().GetAllAsync();

            if (agents == null || !agents.Any())
            {
                return BaseResponse<IEnumerable<AgentDto>>.NotFound();
            }

            var agentsDto = agents.Select(a => new AgentDto(
                a.Name,
                a.Email,
                a.Phone,
                a.ServiceArea,
                a.ImageUrl,
                a.Agency?.Name ?? string.Empty,
                a.Address?.Country ?? string.Empty,
                a.Address?.City ?? string.Empty,
                a.Address?.Street ?? string.Empty,
                a.Address?.ZipCode ?? string.Empty
            ));

            
            var agentsCount = await _unitOfWork.GetRepository<Agent>().CountAsync();
            
            return BaseResponse<IEnumerable<AgentDto>>.Success(agentsDto, "Agents Retrieved Successful", agentsCount);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<AgentDto>>.InternalError(e.Message);
        }
    }
}