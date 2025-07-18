using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Handler;

public class DeleteCategoryHandler(IUnitOfWork unitOfWork, ILogger<Category>  logger): IRequestHandler<DeleteCategoryRequest, BaseResponse<Unit>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork; 
    private readonly ILogger<Category>  _logger = logger;
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The Id Is Valid.
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("request.Id cannot be less or equal to 0");
            }

            // 2. Get Category By ID.
            var existingCategory = await _unitOfWork.GetRepository<Category>().GetByIdAsync(request.Id);

            // 3. Check If The Category Is Null.
            if (existingCategory is null)
            {
                return BaseResponse<Unit>.NotFound("Category not found");
            }
            
            // 4. Delete Category and SaveChanges.
            _unitOfWork.GetRepository<Category>().Delete(request.Id);

            await _unitOfWork.SaveChangesAsync();
            
            // 5. Return NoContent Response: 204.
            return BaseResponse<Unit>.NoContent($"Category with Id: {request.Id} Deleted Successfully");
        }
        catch (Exception e)
        { 
            _logger.LogError("Internal Server Error: {EMessage}", e.Message); 
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}