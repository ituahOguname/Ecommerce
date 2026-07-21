namespace Domain.ValueObjects;

public sealed class Address : IEquatable<Address>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Line1 { get; }
    public string? Line2 { get; }
    public string City { get; }
    public string? State { get; }
    public string PostalCode { get; }
    public string CountryCode { get; }
    public string? Phone { get; }
    
    private Address() {} 
    
    public Address(string firstName, string lastName, string line1, string? line2, string city, string? state, string postalCode, string countryCode, string? phone)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required.", nameof(lastName));
        if (string.IsNullOrWhiteSpace(line1)) throw new ArgumentException("Address line 1 is required.", nameof(line1));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City is required.", nameof(city));
        if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code is required.", nameof(postalCode));
        if (string.IsNullOrWhiteSpace(countryCode) || countryCode.Length != 2) throw new ArgumentException("Country code must be a 2-letter ISO code.", nameof(countryCode));
        if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone number is required.", nameof(phone));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Line1 = line1.Trim();
        Line2 = line2?.Trim();
        City = city.Trim();
        State = state?.Trim();
        PostalCode = postalCode.Trim();
        CountryCode = countryCode.ToUpperInvariant();
        Phone = phone?.Trim();
    }
    
    public string FullName => $"{FirstName} {LastName}".Trim();
    
    public string Format() => string.Join(",", new[]
    {
        Line1, Line2, City, State, PostalCode, CountryCode
    }.Where(s => !string.IsNullOrWhiteSpace(s)));

    public bool Equals(Address? other)
    {
        if (other is null) return false;
        return Line1 == other.Line1 &&
               Line2 == other.Line2 &&
               City == other.City &&
               State == other.State &&
               PostalCode == other.PostalCode &&
               CountryCode == other.CountryCode;
    }
    
    public override bool Equals(object? obj) => Equals(obj as Address);
    public override int GetHashCode() =>
        HashCode.Combine(Line1, Line2, City, State, PostalCode, CountryCode);
    public override string ToString() => Format();
}