using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.Commands.Requests;
using RealEstate.Application.Features.PropertyFeature.Mapping;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Handler;

public class UpdatePropertyHandler(IUnitOfWork unitOfWork, ILogger<Property> logger): 
    IRequestHandler<UpdatePropertyRequest, BaseResponse<Unit>>
{
    #region Create Instances and Inject them into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Property> _logger = logger;
    #endregion

    public async Task<BaseResponse<Unit>> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id < 0) return BaseResponse<Unit>.ValidationError("id < 0");
            
            if (request.PropertyDto == null) return BaseResponse<Unit>.BadRequest("PropertyDto is null");

            var property = await _unitOfWork.GetRepository<Property>().GetByIdAsync(request.Id);

            var agent = await _unitOfWork.GetAgentRepository.GetAgentByName(request.PropertyDto.AgentName);
            
            var category = await _unitOfWork.GetCategoryRepository.GetCategoryByNameAsync(request.PropertyDto.CategoryName);
            
            if (property == null) return BaseResponse<Unit>.BadRequest("Property not found");
            if (agent == null) return BaseResponse<Unit>.BadRequest("Agent not found");
            if (category == null) return BaseResponse<Unit>.BadRequest("Category not found");

            #region MAPPING
            // property.Name = request.PropertyDto.Name ?? property.Name;
            // property.Description = request.PropertyDto.Description ?? property.Description;
            // property.Type = request.PropertyDto.Type;
            // property.Status = request.PropertyDto.Status;
            // property.Price = request.PropertyDto.Price ?? property.Price;
            // property.Rating = request.PropertyDto.Rating ?? property.Rating;
            // property.ListedDate = request.PropertyDto.ListedDate ?? property.ListedDate;
            // property.ImageUrls = request.PropertyDto.ImageUrls ?? property.ImageUrls;
            // property.CategoryId = category.Id;
            // property.AgentId = agent.Id;
            #endregion

            property.To_UpdatePropertyDto(agent, category);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Property with Id: {request.Id} updated successfully");

        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}