using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository;

public class PropertyRepository(ApplicationContext dbContext)
    : GenericRepository<Property>(dbContext), IPropertyRepository
{
    #region Instance Fields
    private readonly ApplicationContext _dbContext = dbContext;
    #endregion
    
    
    #region GET METHODS
    public IQueryable<Property> SearchPropertyByName(string name)
    {
        return _dbContext.Properties.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
    }

    public IQueryable<Property> SearchPropertyByType(PropertyType type)
    {
        return _dbContext.Properties.Where(p => p.Type == type);
    }

    public IQueryable<Property> SearchPropertyByStatus(PropertyStatus status)
    {
        return _dbContext.Properties.Where(p => p.Status == status);
    }

    public IQueryable<Property> SearchPropertyByPrice(decimal fromPrice, decimal toPrice)
    {
        return _dbContext.Properties.Where(p => p.Price >= fromPrice && p.Price <= toPrice);
    }

    public IQueryable<Property> SearchPropertyByRating(double rating)
    {
        return _dbContext.Properties.Where(p => p.Rating.Equals(rating));
    }

    public IQueryable<Property> SearchPropertyByListedDate(DateOnly listedDate)
    {
        // since the ListedDate in Properties Entity is DateTime.
        // and I want to search as a DateOnly.
        // converting from DateOnly to DateTime.
        var startDate = listedDate.ToDateTime(TimeOnly.MinValue);
        var endDate = startDate.AddDays(1);
        
        return _dbContext.Properties.Where(p => p.ListedDate >= startDate && p.ListedDate < endDate);
    }

    public IQueryable<Property> SearchPropertyByCountry(string country)
    {
        return _dbContext.Properties.Where(p => p.Address.Country.Equals(country));
    }

    public IQueryable<Property> SearchPropertyByCity(string city)
    {
        return _dbContext.Properties.Where(p => p.Address.City.ToLower().Equals(city.ToLower()));
    }
    
    public async Task<Property> GetPropertyByNameAsync(string name)
    {
        return (await _dbContext.Properties.FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower()))!;
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _dbContext.Properties
            .Include(p => p.Agent)
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetPropertyCountPerTypeAsync()
    {
        var grouped  = await _dbContext.Properties
            .GroupBy(p => p.Type)
            .Select(g => new { Type = g.Key.ToString(), Count = g.Count() })
            .ToDictionaryAsync(g => g.Type, g => g.Count);
        
        foreach (var type in Enum.GetNames(typeof(PropertyType)))
        {
            if (!grouped.ContainsKey(type))
                grouped[type] = 0;
        }

        return grouped;
    }

    public async Task<Dictionary<string, int>> GetPropertyCountPerStatusAsync()
    {
        var grouped = await _dbContext.Properties
            .GroupBy(p => p.Status)
            .Select(g => new {Status = g.Key.ToString(), Count = g.Count()})
            .ToDictionaryAsync(g => g.Status, g => g.Count);

        foreach (var status in Enum.GetNames(typeof(PropertyStatus)))
        {
            if (!grouped.ContainsKey(status))
            {
                grouped[status] = 0;
            }
        }

        return grouped;
    }

    #endregion
}