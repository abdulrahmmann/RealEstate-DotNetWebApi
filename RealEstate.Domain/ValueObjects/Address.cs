namespace RealEstate.Domain.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }

    private Address () { }

    public Address(string street, string city, string country, string zipcode)
    {
        Street = street;
        City = city;
        Country = country;
        ZipCode = zipcode;
    }

    // protected override IEnumerable<object> GetEqualityComponents()
    // {
    //     yield return Street;
    //     yield return City;
    //     yield return Country;
    //     yield return ZipCode;
    // }
}