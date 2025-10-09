namespace HireUP.Domain.Entities
{
    public class Attachment
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }

        private Attachment(string title, string url)
        {
            Title = title;
            Url = url;
        }

        public static Attachment Create(string title, string url)
        {
            return new Attachment(title, url);
        }
    }
}
