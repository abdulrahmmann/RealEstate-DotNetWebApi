namespace RealEstate.Domain.IRepository;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();

    T GetById(int id);
    Task<T> GetByIdAsync(int id);

    void Add(T entity);
    Task AddAsync(T entity);
    
    Task AddRangeAsync(IEnumerable<T> entities);

    void Delete(int id);

    void Save();
}