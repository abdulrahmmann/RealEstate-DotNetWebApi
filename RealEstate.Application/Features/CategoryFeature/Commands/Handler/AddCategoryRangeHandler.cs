using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Application.Features.CategoryFeature.DTOs;
using RealEstate.Application.Features.CategoryFeature.Mapping;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Handler;

public class AddCategoryRangeHandler(IUnitOfWork unitOfWork, ILogger<Category>  logger): IRequestHandler<AddCategoryRangeRequest,BaseResponse<Unit>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Category>  _logger = logger;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(AddCategoryRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Create List Of Categories to Add Just The Valid Items.
            var categoriesToAdd  = new List<Category>();
            
            // 2. Iterate to The List.
            foreach (var dto in request.CategoriesDto)
            {
                // 3. Check If The Category Is Exist.
                var existingCategory = await _unitOfWork.Context.Categories.FirstOrDefaultAsync(c => c.Name == dto.Name, cancellationToken);
                
                // 4. continue if Not Exist.
                if (existingCategory != null)
                {
                    continue;
                }
                else
                {
                    // 5. Add The Valid Category.
                    var category = dto.To_AddCategoryEntity();
                    categoriesToAdd.Add(category);
                }
            }

            // 6. Check If The List Is Empty.
            if (!categoriesToAdd.Any())
            {
                return BaseResponse<Unit>.Conflict("Categories Failed to Add");
            }

            // 7. Add Categories and SaveChanges.
            await _unitOfWork.GetRepository<Category>().AddRangeAsync(categoriesToAdd);
            
            await _unitOfWork.SaveChangesAsync();
            
            // 8. Return Created Response: 201.
            return BaseResponse<Unit>.Created("Categories Successfully Added");
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}