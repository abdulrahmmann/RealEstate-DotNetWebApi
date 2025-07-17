using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Features.CategoryFeature.Commands.Requests;
using RealEstate.Application.Features.CategoryFeature.DTOs;
using RealEstate.Application.Features.CategoryFeature.Queries.Requests;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : AppControllerBase
    {
        #region GET
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new GetCategoriesRequest());

            return NewResult(result);
        }
        #endregion
        
        
        #region POST
        [HttpPost]
        [Route("add-category")]
        public async Task<IActionResult> AddCategory(AddCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddCategoryRequest(categoryDto));

            return NewResult(result);
        }
        
        [HttpPost]
        [Route("add-categories")]
        public async Task<IActionResult> AddCategoriesRange(IEnumerable<AddCategoryDto> categoriesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new AddCategoryRangeRequest(categoriesDto));

            return NewResult(result);
        }
        #endregion
        
        
        #region DELETE
        [HttpDelete]
        [Route("delete-category/id={id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Mediator.Send(new DeleteCategoryRequest(id));

            return NewResult(result);
        }
        #endregion
    }
}
