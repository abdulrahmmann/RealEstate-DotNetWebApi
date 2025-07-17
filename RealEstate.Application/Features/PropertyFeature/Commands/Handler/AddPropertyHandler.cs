using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Handler;

public class AddPropertyHandler(IUnitOfWork unitOfWork, IValidator<AddPropertyDto> validator): 
    IRequestHandler<AddPropertyRequest, BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<AddPropertyDto> _validator = validator;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(AddPropertyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.PropertyDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(";", validationResult.Errors.Select(err => err.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }

            var existingProperty = await _unitOfWork.GetPropertyRepository.GetPropertyByName(request.PropertyDto.Name);
            
            if (existingProperty != null) return BaseResponse<Unit>.Conflict("Property is already exists");

            var req = request.PropertyDto;

            var agent = await _unitOfWork.GetAgentRepository.GetAgentByName(req.AgentName);

            var category = await _unitOfWork.GetCategoryRepository.GetCategoryByNameAsync(request.PropertyDto.CategoryName);
            
            if (agent == null) return BaseResponse<Unit>.NotFound("Agent not found! Create New Agent");
            
            if (category == null) return BaseResponse<Unit>.NotFound("Category not found! Create New Category");

            var newProperty = new Property {
                Name = req.Name,
                Description = req.Description,
                Type = req.Type,
                Status = req.Status,
                Price = req.Price,
                Rating = req.Rating,
                ListedDate = req.ListedDate,
                ImageUrls = req.ImageUrls,
                CategoryId = category.Id,
                AgentId = agent.Id,
                
                Address = new Address(req.Address.Country, req.Address.City, req.Address.Street, req.Address.ZipCode),
                
                Amenities = new Amenities(
                    req.Amenities.HasAirConditioning,
                    req.Amenities.HasHeating,
                    req.Amenities.IsFurnished,
                    req.Amenities.HasSwimmingPool,
                    req.Amenities.HasFireplace,
                    req.Amenities.HasGarden,
                    req.Amenities.HasSecuritySystem,
                    req.Amenities.HasSmokingArea,
                    req.Amenities.HasParking
                ),
                
                Facilities = new Facilities(
                    req.Facilities.Area,
                    req.Facilities.Rooms,
                    req.Facilities.Kitchens,
                    req.Facilities.Balconies,
                    req.Facilities.Baths,
                    req.Facilities.Beds
                )
            };

            await _unitOfWork.GetRepository<Property>().AddAsync(newProperty);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"New Property with name: {newProperty.Name} Created Successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}