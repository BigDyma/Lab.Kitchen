using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Models
{
    public class Food : BaseEntity
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public int Complexity { get; set; }
        public CookingApparatusType? CookingApparatusType { get; set; }
        public CookingApparatus CookingApparatus { get; set; }
    }
}
