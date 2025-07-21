using RealEstate.Domain.Common;
using RealEstate.Domain.ValueObjects;

namespace RealEstate.Domain.Entities;

public class Agent: BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public string Phone { get; set; } = null!;
    
    public string ServiceArea { get; set; } = null!;
    
    public string ImageUrl { get; set; } = null!;   
    
    public bool IsDeleted { get; set; } = false;
    
    
    // VALUE OBJECTS
    public Address Address { get; set; } = null!; 
    
    
    // FOREIGN KEYS && NAVIGATIONS
    public int? AgencyId { get; set; } 

    public Agency? Agency { get; set; } 

    public ICollection<Property> Properties { get; set; } = [];
    
    public ICollection<User> Clients { get; set; } = [];
}