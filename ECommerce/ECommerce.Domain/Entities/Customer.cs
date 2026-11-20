using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    // ==========================================
    // 1) Anemic Domain Model (Active)
    // ==========================================
    public sealed class Customer : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

/*
// ==========================================
// 2) Rich Domain Model (DDD Pattern)
// ==========================================
namespace ECommerce.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        private readonly List<Order> _orders = new();

        private Customer() { }

        private Customer(string firstName, string lastName, string email, string? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string? PhoneNumber { get; private set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        public static Customer Create(string firstName, string lastName, string email, string? phoneNumber = null)
        {
            ValidateName(firstName, "First name");
            ValidateName(lastName, "Last name");
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            return new Customer(firstName, lastName, email.Trim().ToLowerInvariant(), phoneNumber?.Trim());
        }

        public void UpdateName(string firstName, string lastName)
        {
            ValidateName(firstName, "First name");
            ValidateName(lastName, "Last name");

            FirstName = firstName;
            LastName = lastName;
            SetUpdatedDate();
        }

        public void UpdateContact(string email, string? phoneNumber)
        {
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            Email = email.Trim().ToLowerInvariant();
            PhoneNumber = phoneNumber?.Trim();
            SetUpdatedDate();
        }

        private static void ValidateName(string name, string paramName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException($"{paramName} is required.");
            }

            if (name.Length > 50)
            {
                throw new DomainException($"{paramName} cannot exceed 50 characters.");
            }
        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("Email is required.");
            }

            if (!email.Contains('@') || !email.Contains('.'))
            {
                throw new DomainException("Invalid email format.");
            }

            if (email.Length > 150)
            {
                throw new DomainException("Email cannot exceed 150 characters.");
            }
        }

        private static void ValidatePhoneNumber(string? phoneNumber)
        {
            if (phoneNumber is not null && phoneNumber.Length > 20)
            {
                throw new DomainException("Phone number cannot exceed 20 characters.");
            }
        }
    }
}
*/
