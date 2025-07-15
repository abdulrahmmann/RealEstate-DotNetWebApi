using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository;

public class AgencyRepository: GenericRepository<Agency>, IAgencyRepository
{
    #region Instance Fields
    private readonly ApplicationContext _dbContext;
    #endregion
    
    #region Constructor
    public AgencyRepository(ApplicationContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion
    
    
    #region GET METHODS
    public IQueryable<Agency> SearchAgencyByName(string name)
    {
        return _dbContext.Agencies.Where(a => EF.Functions.Like(a.Name,  $"%{name}%"));
    }

    public async Task<Agency> GetAgencyByLicenseNumber(string licenseNumber)
    {
        return (await _dbContext.Agencies.FirstOrDefaultAsync(a => a.LicenseNumber == licenseNumber))!;
    }

    public async Task<Agency> GetAgencyByTaxNumber(string taxNumber)
    {
        return (await _dbContext.Agencies.FirstOrDefaultAsync(a => a.TaxNumber == taxNumber))!;
    }
    #endregion
    
    
    #region POST METHODS
    public async Task AddAgencyRange(IEnumerable<Agency> agencies)
    {
        await _dbContext.Agencies.AddRangeAsync(agencies);
    }
    #endregion
    

    #region PUT METHODS
    public void UpdateAgency(int id, Agency agency)
    {
        var existingAgency = _dbContext.Agencies.Find(id);
        
        if (existingAgency == null) return;

        existingAgency.Name = agency.Name;
        existingAgency.LicenseNumber = agency.LicenseNumber;
        existingAgency.TaxNumber = agency.TaxNumber;

        _dbContext.Agencies.Update(existingAgency);
    }
    #endregion
    
    
    #region DELETE METHODS
    
    #endregion
    
}