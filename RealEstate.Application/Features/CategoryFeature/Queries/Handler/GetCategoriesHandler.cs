using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.DTOs;
using RealEstate.Application.Features.CategoryFeature.Mapping;
using RealEstate.Application.Features.CategoryFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.CategoryFeature.Queries.Handler;

public class GetCategoriesHandler(IUnitOfWork unitOfWork, ILogger<Category>  logger)
    : IRequestHandler<GetCategoriesRequest, BaseResponse<IEnumerable<CategoryDto>>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Category>  _logger = logger;
    #endregion
    
    public async Task<BaseResponse<IEnumerable<CategoryDto>>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Get All Categories.
            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();

            // 2. Map Categories To CategoriesDTOs.
            var enumerable = categories.ToList();
            var mapped = enumerable.To_CategoryDto_List();
            
            // 3. Get Categories Count.
            var counts = enumerable.Count;
            
            // 4. Return Categories.
            return BaseResponse<IEnumerable<CategoryDto>>.Success(mapped, "Categories Retrieved Successfully", counts);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<IEnumerable<CategoryDto>>.InternalError(e.Message);
        }
    }
}