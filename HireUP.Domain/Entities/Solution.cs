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
    }
}
