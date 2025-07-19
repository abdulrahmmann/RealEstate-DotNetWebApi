using RealEstate.Application.Features.CategoryFeature.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.CategoryFeature.Mapping;

public static class CategoryMapper
{
    #region Map from Category Entity to CategoryDto 
    public static CategoryDto To_CategoryDto(this Category category)
    {
        return new CategoryDto(category.Id, category.Name, category.Description);
    }
    
    public static IEnumerable<CategoryDto> To_CategoryDto_List(this IEnumerable<Category> categories)
    {
        return categories.Select(c => c.To_CategoryDto());
    }
    #endregion

    
    #region Map from Category DTO to Category Entity 
    public static Category To_CategoryEntity(this CategoryDto dto)
    {
        return new Category()
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }
    #endregion


    #region Map from AddCategoryDto to Category Entity 
    public static Category To_AddCategoryEntity(this AddCategoryDto category)
    {
        return new Category()
        {
            Name = category.Name,
            Description = category.Description
        };
    }
    
    public static IEnumerable<Category> To_AddCategoryEntity_List(this IEnumerable<AddCategoryDto> categories)
    {
        return categories.Select(c => c.To_AddCategoryEntity());
    }
    #endregion
}