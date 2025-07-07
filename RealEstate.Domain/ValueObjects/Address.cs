namespace RealEstate.Domain.ValueObjects;

public class Address
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string? Street { get; private set; }
    public string? ZipCode { get; private set; }

    private Address () { }

    public Address(string country, string city, string? street, string? zipcode)
    {
        Country = country;
        City = city;
        Street = street;
        ZipCode = zipcode;
    }

    public Address(string country, string city)
    {
        Country = country;
        City = city;
    }

    // protected override IEnumerable<object> GetEqualityComponents()
    // {
    //     yield return Street;
    //     yield return City;
    //     yield return Country;
    //     yield return ZipCode;
    // }
}