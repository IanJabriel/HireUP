namespace HireUP.Domain.Entities
{
public class Experience
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ExperienceType Type { get; private set; }

    public Experience(string title, string description, ExperienceType type)
    {
        Title = title;
        Description = description;
        Type = type;
    }
}

    public enum ExperienceType
    {
        Out,
        In
    }
}