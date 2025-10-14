namespace HireUP.Domain.Entities
{
    public class Employer
    {
        public int Id { get; private set; }
        public string? GeoLocationId { get; private set; }
        public Attachment? PhotoId { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Document { get; private set; }
        public string? Description { get; private set; }
        public string? Role { get; private set; }
            public List<Skill> Skill { get; private set; }
        public List<Experience> Experience { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Employer(
            string name,
            string phone,
            string email,
            string password,
            string document,
            string? geoLocationId = null,
            Attachment? photoId = null,
            string? description = null,
            string? role = null,
            List<Skill>? skill = null,
            List<Experience>? experience = null,
            DateTime? createdAt = null
            )
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Document = document ?? throw new ArgumentNullException(nameof(document));
            GeoLocationId = geoLocationId;
            PhotoId = photoId;
            Description = description;
            Role = role;
            Skill = skill ?? new List<Skill>();
            Experience = experience ?? new List<Experience>();
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}