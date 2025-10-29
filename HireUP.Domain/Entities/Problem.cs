namespace HireUP.Domain.Entities
{
    public class Problem
    {
        public int Id { get; private set; }
        public int EnterpriseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Problem()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

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

        public void Update(string? title = null, string? description = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;
            
            if (!string.IsNullOrWhiteSpace(description))
                Description = description;
        }
    }
}
