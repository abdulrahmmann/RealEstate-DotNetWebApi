using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Category: BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    public ICollection<Property> Properties { get; set; } = [];
}