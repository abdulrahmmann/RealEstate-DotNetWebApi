using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class Review: BaseEntity
{
    public double Rating { get; set; }
    
    public string Comment { get; set; } = null!;
    
    public DateTime DatePosted { get; set; } = DateTime.UtcNow;
    
    // FOREIGN KEYS && NAVIGATIONS
    public int UserId { get; set; }
    
    public User User { get; set; }  = null!;
    
    public int PropertyId { get; set; }

    public Property Property { get; set; } = null!;
}