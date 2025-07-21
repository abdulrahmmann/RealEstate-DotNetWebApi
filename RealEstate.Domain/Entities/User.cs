using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Enums;
using RealEstate.Domain.ValueObjects;

namespace RealEstate.Domain.Entities;

public class User: IdentityUser<int>
{
    public string Gender { get; init; } = null!;     
    
    public DateOnly BirthDate { get; init; }

    public Address Address { get; init; } = null!;
    
    public bool IsDeleted { get; set; } = false;
    
    public int? AgentId { get; init; }

    // public UserType UserType { get; } = UserType.User;
    
    public string UserType { get; init; } = "User";
    
    public Agent? Agent { get; init; } 

    public ICollection<Review> Reviews { get; set; } = [];
}