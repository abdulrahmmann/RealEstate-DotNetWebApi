using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public class GetAllAgentsQueryHandler: IRequestHandler<GetAllAgentsQuery, BaseResponse<IEnumerable<GetAgentDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAllAgentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<GetAgentDto>>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
    {
        var agents = await _unitOfWork.Context.Agents.Include(a => a.Agency).ToListAsync(cancellationToken);
        
        var mapped = agents.Select(agent => new GetAgentDto(
            Name: agent.Name,
            Email: agent.Email,
            Phone: agent.Phone,
            ServiceArea: agent.ServiceArea,
            Country: agent.Address.Country,
            City: agent.Address.City,
            AgencyName: agent.Agency?.Name ?? "N/A"
        )).ToList();

        return BaseResponse<IEnumerable<GetAgentDto>>.Success(mapped);
    }
}