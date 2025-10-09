namespace HireUP.Domain.Entities
{
    public class Problem
    {
        public int Id { get; private set; }
        public int EnterpriseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Problem(
            int enterpriseId,
            string title,
            string description,
            DateTime? createdAt = null
            )
        {
            EnterpriseId = enterpriseId;
            Title = title;
            Description = description;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}
