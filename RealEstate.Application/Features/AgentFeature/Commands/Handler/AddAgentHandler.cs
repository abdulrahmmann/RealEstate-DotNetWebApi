using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.Commands.Requests;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Commands.Handler;

public class AddAgentHandler(IUnitOfWork unitOfWork, IValidator<AddAgentDto> validator): IRequestHandler<AddAgentRequest, BaseResponse<int>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<AddAgentDto> _validator = validator;
    #endregion
    
    public async Task<BaseResponse<int>> Handle(AddAgentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.AgentDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(";", validationResult.Errors.Select(err => err.ErrorMessage));
                return BaseResponse<int>.ValidationError(errors);
            }
            
            var req = request.AgentDto;

            var isAgencyExist = _unitOfWork.GetAgencyRepository.SearchAgencyByName(req.AgencyName).FirstOrDefault();

            if (isAgencyExist == null)
            {
                return BaseResponse<int>.NotFound("Agency Not Found!! Create Agency");
            }
            
            
            var agent = new Agent
            {
                Name = req.Name,
                Email = req.Email,
                Phone = req.Phone,
                ServiceArea = req.ServiceArea,
                ImageUrl = req.ImageUrl,
                Address = new Address(req.Country, req.City, req.Street, req.ZipCode),
                AgencyId = isAgencyExist!.Id
            };
            
            await _unitOfWork.GetRepository<Agent>().AddAsync(agent);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<int>.Created($"Agent with Id: {agent.Id} Created Successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<int>.InternalError(e.Message);
        }
    }
}