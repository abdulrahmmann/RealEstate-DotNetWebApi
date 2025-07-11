using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.UOF;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : class;
    
    ApplicationContext Context { get; }
    
    IAgencyRepository GetAgencyRepository { get; }
    
    IAgentRepository GetAgentRepository { get; }
    
    void SaveChanges();

    Task SaveChangesAsync();
}