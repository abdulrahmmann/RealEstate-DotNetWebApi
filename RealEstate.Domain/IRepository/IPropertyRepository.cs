using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;

namespace RealEstate.Domain.IRepository;

public interface IPropertyRepository: IGenericRepository<Property>
{
    #region GET METHODS
    IQueryable<Property> SearchPropertyByName(string name);
    
    IQueryable<Property> SearchPropertyByType(PropertyType type);
    
    IQueryable<Property> SearchPropertyByStatus(PropertyStatus status);
    
    IQueryable<Property> SearchPropertyByPrice(decimal price);

    IQueryable<Property> SearchPropertyByRating(double rating);
    
    IQueryable<Property> SearchPropertyByListedDate(DateOnly listedDate);
    
    IQueryable<Property> SearchPropertyByCountry(string country);
    
    IQueryable<Property> SearchPropertyByCity(string city);
    #endregion
    
    #region POST METHODS
    
    #endregion

    #region PUT METHODS
    
    #endregion
    
    #region DELETE METHODS
    
    #endregion
}