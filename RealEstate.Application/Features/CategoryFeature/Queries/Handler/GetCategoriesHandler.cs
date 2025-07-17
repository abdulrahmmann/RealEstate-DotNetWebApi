using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.DTOs;
using RealEstate.Application.Features.CategoryFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Queries.Handler;

public class GetCategoriesHandler(IUnitOfWork unitOfWork): IRequestHandler<GetCategoriesRequest, BaseResponse<IEnumerable<CategoryDto>>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<IEnumerable<CategoryDto>>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();

            var mapped = categories.Select(c => new CategoryDto(c.Id, c.Name, c.Description));

            var counts = categories.Count();
            
            return BaseResponse<IEnumerable<CategoryDto>>.Success(mapped, "Categories Retrieved Successfully", counts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<CategoryDto>>.InternalError(e.Message);
        }
    }
}