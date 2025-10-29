namespace HireUP.Domain.Entities
{
    public class Enterprise
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
        public string? Field { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Enterprise()
        {
            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Document = string.Empty;
        }

        public Enterprise(
            string name,
            string phone,
            string email,
            string password,
            string document,
            string? geoLocationId = null,
            string? description = null,
            string? field = null,
            DateTime? createdAt = null
            )
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Document = document ?? throw new ArgumentNullException(nameof(document));
            GeoLocationId = geoLocationId;
            Description = description;
            Field = field;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }

        public void Update(
            string? name = null,
            string? phone = null,
            string? email = null,
            string? password = null,
            string? geoLocationId = null,
            string? description = null,
            string? field = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            
            if (!string.IsNullOrWhiteSpace(phone))
                Phone = phone;
            
            if (!string.IsNullOrWhiteSpace(email))
                Email = email;
            
            if (!string.IsNullOrWhiteSpace(password))
                Password = password;
            
            if (geoLocationId != null)
                GeoLocationId = geoLocationId;
            
            if (description != null)
                Description = description;
            
            if (field != null)
                Field = field;
        }
    }
}
