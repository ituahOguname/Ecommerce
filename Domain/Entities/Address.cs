using Domain.Common;

namespace Domain.Entities;

public class Address : BaseEntity
{
    public Guid UserId { get; private set; }
    public string? Label { get; private set; }  // Home, Work ...etc
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Line1 { get; private set; } = string.Empty;
    public string? Line2 { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string? State { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    public string CountryCode { get; private set; } = string.Empty;
    public string? Phone { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; } = false;
    
    //-------------- Navigation Properties --------------
    public User User { get; private set; } = null!;
    
    //-------------- EF Core Constructor --------------
    private Address () { }

    public Address(Guid userId, string firstName, string lastName, string line1, string? line2, string city,
        string? state, string postalCode, string countryCode, string? label = null, string? phone = null)
    {
        UserId = userId;
        FirstName   = firstName.Trim();
        LastName    = lastName.Trim();
        Line1       = line1.Trim();
        Line2       = line2?.Trim();
        City        = city.Trim();
        State       = state?.Trim();
        PostalCode  = postalCode.Trim();
        CountryCode = countryCode.Trim().ToUpperInvariant();
        Label       = label?.Trim();
        Phone       = phone?.Trim();
    }

    // to set as default address
    public void SetAsDefault()
    {
        IsDefault = true;
        UpdatedAt = DateTime.UtcNow;
    }

    // to remove from default address
    public void UnsetDefault()
    {
        IsDefault = false;
        UpdatedAt = DateTime.UtcNow;
    }
    
    //-------------- Computed --------------
    // Address entity has a real-world scenario so it takes firstName and lastName
    // in the even that the delivery address differs from the owner of the account 
    public string FullName => $"{FirstName} {LastName}".Trim();
}