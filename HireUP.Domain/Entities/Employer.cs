namespace HireUP.Domain.Entities
{
    public class Employer
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
        public string Role { get; private set; }
        public List<Skill> Skill { get; private set; }
        public List<Experience> Experience { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Employer(
            string geoLocationId,
            Attachment photoId,
            string name,
            string phone,
            string email,
            string password,
            string document,
            string description,
            string role,
            List<Skill> skill,
            List<Experience> experience,
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
            Role = role;
            Skill = skill;
            Experience = experience;
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}