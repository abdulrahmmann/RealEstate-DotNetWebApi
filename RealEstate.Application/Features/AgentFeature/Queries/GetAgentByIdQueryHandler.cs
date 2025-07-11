using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public class GetAgentByIdQueryHandler: IRequestHandler<GetAgentByIdQuery, BaseResponse<GetAgentDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAgentByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<GetAgentDto>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == null)
            {
                return BaseResponse<GetAgentDto>.BadRequest("Request Can not be null");
            }

            if (request.Id <= 0)
            {
                return BaseResponse<GetAgentDto>.ValidationError("Request Can not be less than or equal zero");
            }

            var agent = await _unitOfWork.GetAgentRepository.GetByIdAsync(request.Id);

            var mapped = new GetAgentDto(
                Name: agent.Name,
                Email: agent.Email,
                Phone: agent.Phone,
                ServiceArea: agent.ServiceArea,
                Country: agent.Address.Country,
                City: agent.Address.City,
                Street: agent.Address.Street!,
                ZipCode: agent.Address.ZipCode!,
                AgencyName: agent.Agency?.Name ?? "N/A"
            );
            
            return BaseResponse<GetAgentDto>.Success(mapped);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<GetAgentDto>.InternalError();
        }
    }
}