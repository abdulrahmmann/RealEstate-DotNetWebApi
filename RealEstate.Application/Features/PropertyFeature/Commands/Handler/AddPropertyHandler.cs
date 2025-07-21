using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Handler;

public class AddPropertyHandler: IRequestHandler<AddPropertyRequest, BaseResponse<Unit>>
{
    #region Create Instances.
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<Property> _logger;
    private readonly IValidator<AddPropertyDto> _validator;
    #endregion

    #region Inject Instances into Constructor.
    public AddPropertyHandler(IUnitOfWork unitOfWork, ILogger<Property> logger, IValidator<AddPropertyDto> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(AddPropertyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Validation Request and Checked.
            var validationResult = await _validator.ValidateAsync(request.PropertyDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(";", validationResult.Errors.Select(err => err.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }

            // 2. Check Existing Property.
            var existingProperty = await _unitOfWork.GetPropertyRepository.GetPropertyByNameAsync(request.PropertyDto.Name);
            
            if (existingProperty != null) return BaseResponse<Unit>.Conflict("Property is already exists");

            var req = request.PropertyDto;

            // 3. Get Agent By Name To Get Their ID.
            var agent = await _unitOfWork.GetAgentRepository.GetAgentByName(req.AgentName);

            // 4.Get Category By Name To Get Their ID.
            var category = await _unitOfWork.GetCategoryRepository.GetCategoryByNameAsync(request.PropertyDto.CategoryName);
            
            // 5. Check Agent and Category NULLs.
            if (agent == null) return BaseResponse<Unit>.NotFound("Agent not found! Create New Agent");
            
            if (category == null) return BaseResponse<Unit>.NotFound("Category not found! Create New Category");

            // 6. Mapping
            var newProperty = req.To_PropertyEntity(agent, category);

            // 7. Add Property and SaveChanges.
            await _unitOfWork.GetRepository<Property>().AddAsync(newProperty);

            await _unitOfWork.SaveChangesAsync();
            
            // 8. Return Created Response.
            return BaseResponse<Unit>.Created($"New Property with name: {newProperty.Name} Created Successfully");

        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}