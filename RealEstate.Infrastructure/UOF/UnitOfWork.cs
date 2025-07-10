using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;
using RealEstate.Infrastructure.Repository;

namespace RealEstate.Infrastructure.UOF;

public class UnitOfWork: IUnitOfWork
{
    #region Instance Fields
    private readonly ApplicationContext  _dbContext;
    private readonly Dictionary<Type, object> _repositories;
    public IAgencyRepository GetAgencyRepository { get; }
    
    public ApplicationContext Context { get; }

    #endregion

    #region Constructor
    public UnitOfWork(ApplicationContext dbContext, IAgencyRepository getAgencyRepository, ApplicationContext context)
    {
        _dbContext = dbContext;
        GetAgencyRepository = getAgencyRepository;
        Context = context;
        _repositories = new Dictionary<Type, object>();
    }
    #endregion
    
    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);
        
        if (!_repositories.ContainsKey(type))
        {
            var repoInstance = new GenericRepository<T>(_dbContext);
            _repositories[type] = repoInstance;
        }

        return (IGenericRepository<T>)_repositories[type];
    }
    
    public void SaveChanges() => _dbContext.SaveChanges();

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public void Dispose() => _dbContext.Dispose();

}