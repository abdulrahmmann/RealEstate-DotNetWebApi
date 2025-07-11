using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public class GetAgentsByNameQueryHandler: IRequestHandler<GetAgentsByNameQuery, BaseResponse<IQueryable<GetAgentDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAgentsByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<IQueryable<GetAgentDto>>> Handle(GetAgentsByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Name == null)
            {
                return BaseResponse<IQueryable<GetAgentDto>>.ValidationError("Request Can not be null");
            }

            var agents =  _unitOfWork.GetAgentRepository.SearchAgentByName(request.Name);

            var mapped = agents.Select(agent => new GetAgentDto(
                agent.Name,
                agent.Email,
                agent.Phone,
                agent.ServiceArea,
                agent.Address.Country,
                agent.Address.City,
                agent.Address.Street!,
                agent.Address.ZipCode!,
                agent.Agency!.Name
            ));
            
            return BaseResponse<IQueryable<GetAgentDto>>.Success(mapped);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IQueryable<GetAgentDto>>.InternalError();
        }
    }
}