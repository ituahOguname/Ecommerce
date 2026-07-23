using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? ShortDescription { get; private set; }
    public string Sku { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public decimal? CompareAtPrice { get; private set; } // this is the original/crossed-out price
    public decimal ?CostPrice { get; private set; } // internal cost. not exposed publicly
    public Guid? CategoryId {get; private set;}
    public Guid? VendorId {get; private set;}
    public bool IsActive { get; private set; } = true;
    public bool IsFeatured { get; private set; } = false;
    public decimal? WeightKg { get; private set; }
    public List<string> Tags { get; private set; } = new();
    public Dictionary<string, string> Metadata { get; private set; } = new();
    
    //-------------- Navigation Properties --------------
    public Category? Category { get; private set; }
    public User? Vendor { get; private set; }

    private readonly List<ProductImage> _images = new();
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    private readonly List<ProductVariant> _variants = new();
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

    private readonly List<Inventory> _inventories = new();
    public IReadOnlyCollection<Inventory>  Inventories => _inventories.AsReadOnly();

    private readonly List<Review> _reviews = new();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    
    //-------------- EF Core Constructor --------------
    private Product() { }
    
    //-------------- Public Constructor --------------
    public Product(string name, string slug, string sku, decimal price, Guid? categoryId = null, Guid? vendorId = null)
    {
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Sku = sku.Trim().ToLowerInvariant();
        Price = price;
        CategoryId = categoryId;
        VendorId = vendorId;
        IsActive = true;
    }
    
    //-------------- Domain Methods --------------
    public void UpdateDetails(string name, string slug, string? description, string? shortDescription, decimal price,
        decimal? compareAtPrice, decimal? costPrice, decimal? weightKg, Guid? categoryId = null)
    {
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description?.Trim();
        ShortDescription = shortDescription?.Trim();
        Price = price;
        CompareAtPrice = compareAtPrice;
        CostPrice = costPrice;
        WeightKg = weightKg;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetTags(IEnumerable<string> tags)
    {
        Tags = tags.Select(t => t.Trim().ToLowerInvariant()).ToList();
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetMetadata(Dictionary<string, string> metadata)
    {
        Metadata = metadata;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Feature()
    {
        IsFeatured = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UnFeature()
    {
        IsFeatured = false;
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
    
    //-------------- Computed --------------
    public bool IsOnSale => CompareAtPrice.HasValue && CompareAtPrice.Value > Price;
    
    public decimal? DiscountPercentage => IsOnSale ? Math.Round((CompareAtPrice!.Value - Price) / CompareAtPrice.Value * 100, 2) : null;
}