using RealEstate.Domain.Entities;

namespace RealEstate.Domain.IRepository;

public interface IAgentRepository: IGenericRepository<Agent>
{
    #region GET METHODS
    Task<IEnumerable<Agent>>  GetAllAgentsAsync();
    
    IQueryable<Agent> SearchAgentByName(string name);
    
    Task<Agent> GetAgentByEmail(string email);
    
    Task<Agent> GetAgentByPhone(string phone);
    #endregion
    
    
    #region POST METHODS
    Task AddAgentRange(IEnumerable<Agent> agents);
    #endregion
    

    #region PUT METHODS
    void UpdateAgent(int id, Agent agent);
    #endregion
    
    
    #region DELETE METHODS
    
    #endregion
}