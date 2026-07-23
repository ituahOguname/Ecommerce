using Domain.Common;

namespace Domain.Entities;

public class Category: BaseEntity
{
    public string Name { get; private set; } =  string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public Guid? ParentCategoryId { get; private set; }
    public string? ImageUrl { get; private set; }
    public int SortOrder { get; private set; } = 0;
    public bool IsActive { get; private set; } = false;
    public bool IsDeleted { get; private set; } = true;
    
    //-------------- Navigation Properties --------------
    
    //self-referencing --> a category can have a parent category
    public Category? ParentCategory { get; private set; }
    
    // a category can have many child categories
    private readonly List<Category> _subCategories = new();
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();
    
    // products that fall into the particular category
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    
    //-------------- EF Core Constructor --------------
    private Category() { }
    
    //-------------- Public Constructor --------------
    public Category(string name, string slug, string? description = null, Guid? parentCategoryId = null)
    {
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description?.Trim();
        ParentCategoryId = parentCategoryId;
        IsActive = true;
    }
    
    //-------------- Domain Methods --------------
    public void Update(string name, string slug, string? description, string? imageUrl, int sortOder)
    {
        Name = name.Trim();
        Slug = slug.Trim().ToLowerInvariant();
        Description = description?.Trim();
        ImageUrl = imageUrl?.Trim();
        SortOrder = sortOder;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetParent(Guid? parentCategoryId)
    {
        ParentCategoryId = parentCategoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SetImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
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
    
    // -------------- Computed --------------
    public bool IsTopLevel => ParentCategoryId is null;
}