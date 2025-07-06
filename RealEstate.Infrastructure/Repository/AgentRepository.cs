using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository;

public class AgentRepository: GenericRepository<Agent>, IAgentRepository
{
    #region Instance Fields
    private readonly ApplicationContext _dbContext;
    #endregion
    
    #region Constructor
    public AgentRepository(ApplicationContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    
    #region GET METHODS
    public IQueryable<Agent> SearchAgentByName(string name)
    {
        return _dbContext.Agents.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
    }

    public async Task<Agent> GetAgentByEmail(string email)
    {
        return (await _dbContext.Agents.FirstOrDefaultAsync(p => p.Email == email))!;
    }

    public async Task<Agent> GetAgentByPhone(string phone)
    {
        return (await _dbContext.Agents.FirstOrDefaultAsync(p => p.Phone == phone))!;
    }
    #endregion
    
    
    #region POST METHODS
    public async Task AddAgentRange(IEnumerable<Agent> agents)
    {
        await _dbContext.Agents.AddRangeAsync(agents);
    }
    #endregion
    
    
    #region PUT METHODS
    
    #endregion
    
    
    #region DELETE METHODS
    
    #endregion
}