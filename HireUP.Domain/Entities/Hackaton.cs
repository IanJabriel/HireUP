namespace HireUP.Domain.Entities
{
    public class Hackaton
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Attachment> Attachments { get; private set; }
        public List<Employer> EmployeesIds { get; private set; }
        public bool Private { get; private set; }
        public string? CodeAcess { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Hackaton(
            string title,
            string description,
            List<Attachment> attachments,
            List<Employer> employeesIds,
            bool isPrivate,
            string? codeAcess,
            DateTime startDate,
            DateTime endDate,
            DateTime? createdAt = null
            )
        {
            Title = title;
            Description = description;
            Attachments = attachments;
            EmployeesIds = employeesIds;
            Private = isPrivate;
            CodeAcess = codeAcess;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}
