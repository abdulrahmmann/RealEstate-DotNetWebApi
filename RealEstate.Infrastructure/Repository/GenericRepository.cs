using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository;

public class GenericRepository<T>: IGenericRepository<T> where T : class
{
    #region Instance Fields
    private readonly ApplicationContext  _dbContext;
    private readonly DbSet<T>  _dbSet;
    #endregion

    public GenericRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public T GetById(int id)
    {
        return _dbSet.Find(id)!;
    }

    public async Task<T> GetByIdAsync(int id) => (await _dbSet.FindAsync(id))!;

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);
    

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(int id)
    {
        var entity = _dbSet.Find(id);
        _dbSet.Remove(entity!);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}