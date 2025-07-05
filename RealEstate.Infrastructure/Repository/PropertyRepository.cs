using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Context;

namespace RealEstate.Infrastructure.Repository;

public class PropertyRepository: GenericRepository<Property>, IPropertyRepository
{
    #region Instance Fields
    private readonly ApplicationContext _dbContext;
    #endregion
    
    #region Constructor
    public PropertyRepository(ApplicationContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    
    #region GET METHODS
    public IQueryable<Property> SearchPropertyByName(string name)
    {
        return _dbContext.Properties.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));
    }

    public IQueryable<Property> SearchPropertyByType(PropertyType type)
    {
        return _dbContext.Properties.Where(p => p.Type.Equals(type));
    }

    public IQueryable<Property> SearchPropertyByStatus(PropertyStatus status)
    {
        return _dbContext.Properties.Where(p => p.Status.Equals(status));
    }

    public IQueryable<Property> SearchPropertyByPrice(decimal price)
    {
        return _dbContext.Properties.Where(p => p.Price.Equals(price));
    }

    public IQueryable<Property> SearchPropertyByRating(double rating)
    {
        return _dbContext.Properties.Where(p => p.Rating.Equals(rating));
    }

    public IQueryable<Property> SearchPropertyByListedDate(DateOnly listedDate)
    {
        // since the ListedDate on Properties Entity is DateTime.
        // and i want to search as a DateOnly.
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
    #endregion
}