namespace RealEstate.Domain.ValueObjects;

public class Facilities
{
    public int Area { get; private set; }      
    public int Rooms { get; private set; }
    public int Kitchens { get; private set; }
    public int Balconies { get; private set; } 
    public int Baths { get; private set; }
    public int Beds { get; private set; }

    private Facilities() { }

    public Facilities(int area, int rooms, int kitchens, int balconies, int baths, int beds)
    {
        Area = area;
        Rooms = rooms;
        Kitchens = kitchens;
        Balconies = balconies;
        Baths = baths;
        Beds = beds;
    }

    // protected override IEnumerable<object> GetEqualityComponents()
    // {
    //     yield return Area;
    //     yield return Rooms;
    //     yield return Kitchens;
    //     yield return Balconies;
    //     yield return Baths;
    //     yield return Beds;
    // }
}