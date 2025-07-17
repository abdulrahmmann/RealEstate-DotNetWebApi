using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Handler;

public class DeleteCategoryHandler(IUnitOfWork unitOfWork): IRequestHandler<DeleteCategoryRequest, BaseResponse<Unit>>
{
    #region INSTANCES

    private readonly IUnitOfWork _unitOfWork = unitOfWork; 
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("request.Id cannot be less or equal to 0");
            }

            var existingCategory = await _unitOfWork.GetRepository<Category>().GetByIdAsync(request.Id);

            if (existingCategory == null)
            {
                return BaseResponse<Unit>.NotFound("Category not found");
            }
            
            _unitOfWork.GetRepository<Category>().Delete(request.Id);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success($"Category with Id: {request.Id} Deleted Successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}