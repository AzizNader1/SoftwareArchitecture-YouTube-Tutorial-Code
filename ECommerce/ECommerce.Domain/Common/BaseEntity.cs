namespace ECommerce.Domain.Common
{
    // ==========================================
    // 1) Anemic Domain Model (Active)
    // ==========================================
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
    }
}

/*
// ==========================================
// 2) Rich Domain Model (DDD Pattern)
// ==========================================
namespace ECommerce.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; protected set; }

        public void SetUpdatedDate()
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
*/
