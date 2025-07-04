using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.ValueObjects;

namespace RealEstate.Domain.Entities;

public class User: IdentityUser<int>
{
    public required string Gender { get; set; }    
    
    public DateOnly BirthDate { get; set; }

    public Address Address { get; set; } = null!;
    
    public int? AgentId { get; set; }       
    
    public Agent? Agent { get; set; }

    public ICollection<Review> Reviews { get; set; } = [];
}