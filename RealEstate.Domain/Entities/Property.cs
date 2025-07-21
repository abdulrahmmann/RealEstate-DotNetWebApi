using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;
using RealEstate.Domain.Common;
using RealEstate.Domain.Enums;
using RealEstate.Domain.ValueObjects;

namespace RealEstate.Domain.Entities;

public class Property: BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PropertyType Type { get; set; }  
    
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PropertyStatus Status { get; set; } 
    
    public decimal Price { get; set; }    
    
    
    public double? Rating { get; set; }     
    
    
    public DateTime ListedDate { get; set; }
    
    
    public List<string> ImageUrls { get; set; } = [];

    public bool IsDeleted { get; set; } = false;
    
    // VALUE OBJECTS

    public Address Address { get; set; } = null!;
    
    public Amenities Amenities { get; set; } = null!; 
    
    public Facilities Facilities { get; set; } = null!;
    
    // FOREIGN KEYS && NAVIGATIONS
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; } = null!;
    
    public ICollection<Review> Reviews { get; set; } = [];
    
    public int AgentId { get; set; }     
    
    public Agent Agent { get; set; } = null!;
}