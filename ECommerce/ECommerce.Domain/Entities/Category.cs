using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    // ==========================================
    // 1) Anemic Domain Model (Active)
    // ==========================================
    public sealed class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

/*
// ==========================================
// 2) Rich Domain Model (DDD Pattern)
// ==========================================
namespace ECommerce.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        private readonly List<Product> _products = new();

        private Category() { }

        private Category(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public static Category Create(string name, string? description)
        {
            ValidateName(name);
            ValidateDescription(description);

            return new Category(name, description);
        }

        public void Rename(string name)
        {
            ValidateName(name);
            Name = name;
            SetUpdatedDate();
        }

        public void UpdateDescription(string? description)
        {
            ValidateDescription(description);
            Description = description;
            SetUpdatedDate();
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Category name is required.");
            }

            if (name.Length > 100)
            {
                throw new DomainException("Category name cannot exceed 100 characters.");
            }
        }

        private static void ValidateDescription(string? description)
        {
            if (description is not null && description.Length > 500)
            {
                throw new DomainException("Category description cannot exceed 500 characters.");
            }
        }
    }
}
*/
