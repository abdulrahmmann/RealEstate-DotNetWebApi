using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public class GetAgentByPhoneQueryHandler: IRequestHandler<GetAgentByPhoneQuery, BaseResponse<GetAgentDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAgentByPhoneQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion

    public async Task<BaseResponse<GetAgentDto>> Handle(GetAgentByPhoneQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PhoneNumber == null)
            {
                return BaseResponse<GetAgentDto>.ValidationError("Request Can not be null");
            }

            var agent = await _unitOfWork.GetAgentRepository.GetAgentByPhone(request.PhoneNumber);
            
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