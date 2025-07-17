using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Application.Features.CategoryFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Handler;

public class AddCategoryRangeHandler(IUnitOfWork unitOfWork): IRequestHandler<AddCategoryRangeRequest,BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(AddCategoryRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var categoriesToAdd  = new List<Category>();
            
            foreach (var dto in request.CategoriesDto)
            {
                var existingCategory = await _unitOfWork.Context.Categories.FirstOrDefaultAsync(c => c.Name == dto.Name, cancellationToken);

                if (existingCategory != null)
                {
                    continue;
                }
                else
                {
                    var category = new Category
                    {
                        Name = dto.Name,
                        Description = dto.Description,
                    };
                    
                    categoriesToAdd.Add(category);
                }
            }

            if (!categoriesToAdd.Any())
            {
                return BaseResponse<Unit>.Conflict("Categories Failed to Add");
            }

            await _unitOfWork.GetRepository<Category>().AddRangeAsync(categoriesToAdd);
            
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created("Categories Successfully Added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}