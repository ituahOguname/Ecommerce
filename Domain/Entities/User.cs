using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : BaseEntity
{
    // Regular Properties
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Phone { get; private set; }
    public UserRole Role { get; private set; } = UserRole.Customer;
    public bool IsEmailVerified { get; private set; } = false;
    public bool IsActive { get; private set; } = false;
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }
    
    
    // Navigation Properties
    private readonly List<Address> _addresses = new();
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    
    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private readonly List<Review> _reviews = new();
    public IReadOnlyCollection<Review>  Reviews => _reviews.AsReadOnly();

    private readonly List<Notification> _notificaations = new();
    public IReadOnlyCollection<Notification> Notificaations => _notificaations.AsReadOnly();

    private readonly List<ChatSession> _chatSessions = new();
    private IReadOnlyCollection<ChatSessiion> _chatSessiions =>  _chatSessions.AsReadOnly();
    
    //-------------- EF Core Constructor --------------
    private User() { }

    //-------------- Public Constructor --------------
    public User(string email, string passwordHash, string firstName, string lastName)
    {
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Role = UserRole.Customer;
        IsActive = true;
    }
    
    //-------------- Below are the domain methods --------------
    public void UpdateProfile(string firstName, string lastName, string? phone)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Phone = phone?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SetRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiry = expiry;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdatePasswordHash(string newHash)
    {
        PasswordHash = newHash;
        UpdatedAt = DateTime.UtcNow;
    }

    // ── Computed ──────────────────────────────────────────────────────────
    public string FullName => $"{FirstName} {LastName}".Trim();
}