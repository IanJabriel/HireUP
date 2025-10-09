namespace HireUP.Domain.Entities
{
    public class Culture
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Icon { get; private set; }

        public Culture(string title, string description, int icon)
        {
            Title = title;
            Description = description;
            Icon = icon;
        }
    }
}