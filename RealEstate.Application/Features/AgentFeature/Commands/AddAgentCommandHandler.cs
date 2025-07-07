using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Commands;

public class AddAgentCommandHandler: IRequestHandler<AddAgentCommand, BaseResponse<int>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IValidator<AddAgentDto> _validator;
    #endregion
    
    #region Constructor
    public AddAgentCommandHandler(IUnitOfWork unitOfWork, IValidator<AddAgentDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<int>> Handle(AddAgentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.AgentDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage)).ToList();
                return BaseResponse<int>.BadRequest($"Validation Error : {validationErrors}");
            }

            var agentDto = request.AgentDto;

            var newAgent = new Agent
            {
                Name = agentDto.Name,
                Email = agentDto.Email,
                Phone = agentDto.Phone,
                ServiceArea = agentDto.ServiceArea,
                ImageUrl = agentDto.ImageUrl,
                Address = new Address(
                    country:agentDto.Country, 
                    city:agentDto.City, 
                    street:agentDto.Street, 
                    zipcode:agentDto.ZipCode),
                AgencyId = agentDto.AgencyId,
            };

            await _unitOfWork.GetRepository<Agent>().AddAsync(newAgent);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<int>.Created("Agent Created Successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine("Add Agent Command Handler Error: " + e.Message);
            return BaseResponse<int>.InternalError();
        }
    }
}