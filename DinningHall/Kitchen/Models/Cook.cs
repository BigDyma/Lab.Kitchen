namespace Kitchen.Models
{
    public class Cook : BaseEntity
    {
        public int Rank { get; set; }
        public int Proficiency { get; set; }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }

        public Cook(): base()
        {

        }
    }
}