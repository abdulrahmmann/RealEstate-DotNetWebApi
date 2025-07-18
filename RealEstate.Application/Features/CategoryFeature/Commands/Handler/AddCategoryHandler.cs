using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;
using RealEstate.Application.Features.CategoryFeature.Mapping;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Handler;

public class AddCategoryHandler(IUnitOfWork unitOfWork,  ILogger<Category>  logger): IRequestHandler<AddCategoryRequest, BaseResponse<Unit>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Category>  _logger = logger;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The request Is Null.
            if (request?.CategoryDto is null)
            {
                return BaseResponse<Unit>.BadRequest("request.CategoryDto can not be null");
            }

            // 2. Check If The Category By Name Is Exist.
            var existingCategory = await _unitOfWork.Context.Categories.FirstOrDefaultAsync(c => c.Name == request.CategoryDto.Name, cancellationToken);

            if (existingCategory != null)
            {
                return BaseResponse<Unit>.Conflict("category already exists");
            }
            
            // 3. Map AddCategoryDTO Request To Category Entity.
            var mapped = request.CategoryDto.To_AddCategoryEntity();
            
            
            // 4. Create Category and SaveChanges.
            await _unitOfWork.GetRepository<Category>().AddAsync(mapped);
            
            await _unitOfWork.SaveChangesAsync();
            
            // 5. Return Created Response: 201.
            return BaseResponse<Unit>.Created($"Category with Name: {request.CategoryDto.Name} Successfully Created");
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}