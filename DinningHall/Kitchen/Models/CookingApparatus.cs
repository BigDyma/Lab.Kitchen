namespace Kitchen.Models
{
    public class CookingApparatus :BaseEntity
    {
        public CookingApparatusType Type { get; set; }
        
        public bool Busy { get; set; }
    }
}