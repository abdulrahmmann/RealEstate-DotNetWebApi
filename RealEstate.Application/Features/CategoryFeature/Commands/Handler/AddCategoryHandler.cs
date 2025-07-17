using MediatR;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Handler;

public class AddCategoryHandler(IUnitOfWork unitOfWork): IRequestHandler<AddCategoryRequest, BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.CategoryDto is null)
            {
                return BaseResponse<Unit>.BadRequest("request.CategoryDto can not be null");
            }

            var existingCategory = await _unitOfWork.Context.Categories.FirstOrDefaultAsync(c => c.Name == request.CategoryDto.Name, cancellationToken);

            if (existingCategory != null)
            {
                return BaseResponse<Unit>.BadRequest("category already exists");
            }

            var mapped = new Category
            {
                Name = request.CategoryDto.Name,
                Description = request.CategoryDto.Description
            };
            
            await _unitOfWork.GetRepository<Category>().AddAsync(mapped);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Created($"Category with Name: {request.CategoryDto.Name} Successfully Created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}