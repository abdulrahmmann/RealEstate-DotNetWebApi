using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository;

public class CategoryRepository(ApplicationContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
{
    #region Instance Fields
    private readonly ApplicationContext _dbContext = dbContext;
    #endregion
    
    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        return (await _dbContext.Categories.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower()))!;
    }
}