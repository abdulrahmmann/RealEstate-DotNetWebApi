using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Commands.Handler;

public class UpdateAgentHandler(IUnitOfWork unitOfWork): IRequestHandler<UpdateAgentRequest, BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(UpdateAgentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("Id can not be less than or equal to zero");
            }

            if (request.AgentDto is null)
            {
                return BaseResponse<Unit>.ValidationError("Request AgentDto can not be null");
            }

            var agency = await _unitOfWork.GetAgencyRepository.SearchAgencyByName(request.AgentDto.AgencyName).FirstOrDefaultAsync(cancellationToken);

            if (agency is null)
            {
                return BaseResponse<Unit>.NotFound("Agency not found");
            }

            var req = request.AgentDto;
            
            var agent = await _unitOfWork.GetRepository<Agent>().GetByIdAsync(request.Id);

            agent.Name = req.Name ?? agent.Name;
            agent.Email = req.Email ?? agent.Email;
            agent.Phone = req.Phone ?? agent.Phone;
            agent.ServiceArea = req.ServiceArea ?? agent.ServiceArea;
            agent.ImageUrl = req.ImageUrl ??  agent.ImageUrl;
            agent.AgencyId = agency?.Id ??  agent.AgencyId;
            agent.Address = new Address(req.Country, req.City, req.Street, req.ZipCode) ?? agent.Address;
            
            _unitOfWork.GetAgentRepository.UpdateAgent(request.Id, agent);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Agent with Id: {request.Id} updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}