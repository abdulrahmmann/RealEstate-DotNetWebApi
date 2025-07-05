using RealEstate.Domain.IRepository;

namespace RealEstate.Infrastructure.UOF;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : class;
    
    void SaveChanges();

    Task SaveChangesAsync();
}