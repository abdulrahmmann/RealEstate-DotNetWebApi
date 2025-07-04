namespace RealEstate.Domain.ValueObjects;

public class Amenities
{
    public bool HasAirConditioning { get; private set; }
    public bool HasHeating { get; private set; }      
    public bool IsFurnished { get; private set; }
    public bool HasSwimmingPool { get; private set; }
    public bool HasFireplace { get; private set; }
    public bool HasGarden { get; private set; }
    public bool HasSecuritySystem { get; private set; }
    public bool? HasSmokingArea { get; private set; }
    public bool HasParking { get; private set; }

    private Amenities() { }

    public Amenities(bool hasAirConditioning, bool hasHeating, bool isFurnished, bool hasSwimmingPool, bool hasFireplace, bool hasGarden, bool hasSecuritySystem, bool? hasSmokingArea, bool hasParking)
    {
        HasAirConditioning = hasAirConditioning;
        HasHeating = hasHeating;
        IsFurnished = isFurnished;
        HasSwimmingPool = hasSwimmingPool;
        HasFireplace = hasFireplace;
        HasGarden = hasGarden;
        HasSecuritySystem = hasSecuritySystem;
        HasSmokingArea = hasSmokingArea;
        HasParking = hasParking;
    }

    // protected override IEnumerable<object> GetEqualityComponents()
    // {
    //     yield return HasAirConditioning;
    //     yield return HasHeating;
    //     yield return IsFurnished;
    //     yield return HasSwimmingPool;
    //     yield return HasFireplace;
    //     yield return HasGarden;
    //     yield return HasSecuritySystem;
    //     yield return HasSmokingArea ?? false;
    //     yield return HasParking;
    // }
}