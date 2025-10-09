namespace HireUP.Domain.Entities
{
    public class Skill
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Skill(string name)
        {
            Name = name;
        }
    }

}
