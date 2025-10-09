namespace HireUP.Domain.Entities
{
    public class Enterprise
    {
        public int Id { get; private set; }
        public string GeoLocationId { get; private set; }
        public Attachment PhotoId { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Document { get; private set; }
        public string Description { get; private set; }
        public string Field { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Enterprise(
            string geoLocationId,
            Attachment photoId,
            string name,
            string phone,
            string email,
            string password,
            string document,
            string description,
            string field,
            DateTime? createdAt = null
            )
        {
            GeoLocationId = geoLocationId;
            PhotoId = photoId;
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
            Document = document;
            Description = description;
            Field = field;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}
