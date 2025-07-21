using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Agency: BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string LicenseNumber { get; set; } = null!;
    
    public string TaxNumber { get; set; } = null!;
    
    public bool IsDeleted { get; set; } = false;

    // FOREIGN KEYS && NAVIGATIONS
    
    public ICollection<Agent> Agents { get; set; } = [];
}