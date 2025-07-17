using RealEstate.Domain.Entities;

namespace RealEstate.Domain.IRepository;

public interface ICategoryRepository: IGenericRepository<Category>
{
    Task<Category>  GetCategoryByNameAsync(string name);
}