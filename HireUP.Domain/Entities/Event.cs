using HireUP.Domain.Entities.Base;

namespace HireUP.Domain.Entities
{
    public class Event : EmployerRegistrableEntity
    {
        public override int Id { get; protected set; }
        public int EnterpriseId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Attachment> Attachments { get; private set; }
        public override List<Employer> EmployeesIds { get; protected set; }
        public bool IsPrivate { get; private set; }
        public string? CodeAcess { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        private Event()
        {
            Title = string.Empty;
            Description = string.Empty;
            Attachments = new List<Attachment>();
            EmployeesIds = new List<Employer>();
        }

        public Event(
            int enterpriseId,
            string title,
            string description,
            DateTime startDate,
            DateTime endDate,
            bool isPrivate = false,
            string? codeAcess = null,
            DateTime? createdAt = null
            )
        {
            EnterpriseId = enterpriseId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            StartDate = startDate;
            EndDate = endDate;
            IsPrivate = isPrivate;
            CodeAcess = codeAcess;
            Attachments = new List<Attachment>();
            EmployeesIds = new List<Employer>();
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }

        public void Update(
            string? title = null,
            string? description = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool? isPrivate = null,
            string? codeAcess = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;
            
            if (!string.IsNullOrWhiteSpace(description))
                Description = description;
            
            if (startDate.HasValue)
                StartDate = startDate.Value;
            
            if (endDate.HasValue)
                EndDate = endDate.Value;
            
            if (isPrivate.HasValue)
                IsPrivate = isPrivate.Value;
            
            if (codeAcess != null)
                CodeAcess = codeAcess;
        }

        protected override string GetEntityTypeName() => "evento";
    }
}