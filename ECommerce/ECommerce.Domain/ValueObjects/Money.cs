using ECommerce.Domain.Common;

namespace ECommerce.Domain.ValueObjects
{
    public sealed record Money(decimal Amount, string Currency)
    {
        public static Money Create(decimal amount, string currency)
        {
            if (amount <= 0)
            {
                throw new DomainException("Price must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            {
                throw new DomainException("Currency must be a 3-letter ISO 4217 code (e.g. USD, EGP, EUR).");
            }

            return new Money(amount, currency.ToUpperInvariant());
        }
    }
}
