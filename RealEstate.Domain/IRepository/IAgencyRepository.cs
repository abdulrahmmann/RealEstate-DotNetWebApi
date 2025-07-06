using RealEstate.Domain.Entities;

namespace RealEstate.Domain.IRepository;

public interface IAgencyRepository: IGenericRepository<Agency>
{
    #region GET METHODS
    IQueryable<Agency> SearchAgencyByName(string name);
    
    Task<Agency> GetAgencyByLicenseNumber(string licenseNumber);
    
    Task<Agency> GetAgencyByTaxNumber(string taxNumber);
    #endregion
    
    
    #region POST METHODS
    Task AddAgencyRange(IEnumerable<Agency> agencies);
    #endregion
    

    #region PUT METHODS
    
    #endregion
    
    
    #region DELETE METHODS
    
    #endregion
}