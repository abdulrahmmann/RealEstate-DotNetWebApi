using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Handler;

public class AddPropertiesRangeHandler: IRequestHandler<AddPropertiesRangeRequest, BaseResponse<Unit>>
{
    #region Create Instances.
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<Property> _logger;
    private readonly IValidator<AddPropertyDto> _validator;
    #endregion
    
    #region Inject Instances into Constructor.
    public AddPropertiesRangeHandler(IUnitOfWork unitOfWork, ILogger<Property> logger, IValidator<AddPropertyDto> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(AddPropertiesRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var propertiesToAdd = new List<Property>();
            
            var invalidProperties = new List<string>();

            foreach (var dto in request.PropertyDto)
            {
                var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
                
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(err => err.ErrorMessage));
                    invalidProperties.Add(errors);
                }
                
                // 3. Check If The Category Is Exist.
                var existingProperty = await _unitOfWork.GetPropertyRepository.GetPropertyByNameAsync(dto.Name);

                var agent = await _unitOfWork.GetAgentRepository.GetAgentByName(dto.AgentName);
                var category = await _unitOfWork.GetCategoryRepository.GetCategoryByNameAsync(dto.CategoryName);
                
                if (agent == null) return BaseResponse<Unit>.NotFound("Agent not found! create new agent");
                if (category == null) return BaseResponse<Unit>.NotFound("Category not found! create new agent");
                
                // 4. continue if Not Exist.
                if (existingProperty != null)
                {
                    continue;
                }
                else
                {
                    // 5. Add The Valid property.
                    var property = dto.To_PropertyEntity(agent, category);
                    propertiesToAdd.Add(property);
                }
            }
            if (!propertiesToAdd.Any())
            {
                return BaseResponse<Unit>.NoContent("Property no content!");
            }

            await _unitOfWork.GetPropertyRepository.AddRangeAsync(propertiesToAdd);
            await _unitOfWork.SaveChangesAsync();
                
            return BaseResponse<Unit>.Success();
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}