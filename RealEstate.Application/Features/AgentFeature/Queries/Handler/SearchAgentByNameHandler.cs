using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Application.Features.AgentFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries.Handler;

public class SearchAgentByNameHandler(IUnitOfWork unitOfWork): IRequestHandler<SearchAgentByNameRequest, BaseResponse<IQueryable<AgentDto>>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    public async Task<BaseResponse<IQueryable<AgentDto>>> Handle(SearchAgentByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BaseResponse<IQueryable<AgentDto>>.ValidationError("request can not be null or empty");
            }
            
            var agents = _unitOfWork.GetAgentRepository.SearchAgentByName(request.Name);

            if (agents.Equals(null))
            {
                return BaseResponse<IQueryable<AgentDto>>.NoContent();
            }
            
            var agentsDto = agents.Select(a => new AgentDto(
                a.Name,
                a.Email,
                a.Phone,
                a.ServiceArea,
                a.ImageUrl,
                a.Agency!.Name,
                a.Address.Country,
                a.Address.City,
                a.Address.Street!,
                a.Address.ZipCode!
            ));
            
            var agentsCount = await _unitOfWork.GetRepository<Agent>().CountAsync();
            
            return BaseResponse<IQueryable<AgentDto>>.Success(agentsDto, $"Agents By Name {request.Name} Retrieved Successful", agentsCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IQueryable<AgentDto>>.InternalError(e.Message);
        }
    }
}