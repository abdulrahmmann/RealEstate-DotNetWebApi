using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.Commands.Requests;
using RealEstate.Application.Features.AgentFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgentFeature.Commands.Handler;

public class AddAgentRangeHandler(IUnitOfWork unitOfWork, IValidator<AddAgentDto> validator)
    : IRequestHandler<AddAgentRangeRequest, BaseResponse<int>>
{
    #region INSTANCES

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<AddAgentDto> _validator = validator;

    #endregion

    public async Task<BaseResponse<int>> Handle(AddAgentRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var invalidItems = new List<string>();

            var validItems = new List<Agent>();

            foreach (var dto in request.AgentsDto)
            {
                var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(err => err.ErrorMessage));
                    invalidItems.Add($"[{dto.Name}]: {errors}");
                }

                var existAgency = _unitOfWork.GetAgencyRepository.SearchAgencyByName(dto.AgencyName).FirstOrDefault();

                if (existAgency.Equals(null))
                {
                    break;
                }

                validItems.Add(new Agent
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    ServiceArea = dto.ServiceArea,
                    ImageUrl = dto.ImageUrl,
                    Address = new Address(dto.Country, dto.City, dto.Street, dto.ZipCode),
                    AgencyId = existAgency.Id
                });
            }
            
            if (validItems.Count != 0) 
            {
                await _unitOfWork.GetRepository<Agent>().AddRangeAsync(validItems); 
                await _unitOfWork.SaveChangesAsync(); 
            }

            var totalCount = await _unitOfWork.GetRepository<Agent>().CountAsync();

            if (invalidItems.Any() && validItems.Any())
            {
                return BaseResponse<int>.Created(
                    validItems.Count,
                    $"Some agents were added, but others failed validation: {string.Join(" | ", invalidItems)}",
                    totalCount
                    );
            }

            if (!validItems.Any())
            {
                return BaseResponse<int>.ValidationError(
                    $"No agencies were added. Errors: {string.Join(" | ", invalidItems)}");
            }

            return BaseResponse<int>.Created(validItems.Count, "All agents created successfully.", totalCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<int>.InternalError(e.Message);
        }
    }
}