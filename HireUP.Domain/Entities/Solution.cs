namespace HireUP.Domain.Entities
{
    public class Solution
    {
        public int Id { get; private set; }
        public int ProblemId { get; private set; }
        public int EmployeeId { get; private set; }
        public string Title { get; private set; }
        public Attachment Attachment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Solution()
        {
            Title = string.Empty;
            Attachment = Attachment.Create(string.Empty, string.Empty);
        }
        public Solution(
            int problemId,
            int employeeId,
            string title,
            Attachment attachment,
            DateTime? createdAt = null
            )
        {
            ProblemId = problemId;
            EmployeeId = employeeId;
            Title = title;
            Attachment = attachment;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }

        public void Update(string? title = null, Attachment? attachment = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;
            
            if (attachment != null)
                Attachment = attachment;
        }
    }
}
