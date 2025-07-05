namespace RealEstate.Domain.IRepository;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();

    T GetById(int id);

    void Add(T entity);

    void Delete(int id);

    void Save();
}