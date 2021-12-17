using Newtonsoft.Json;
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

        public string CookingApparatusTypeName { get; set; }
        
        public CookingApparatusType? CookingApparatusType => SetCookingApparatus();
        
        private CookingApparatusType? SetCookingApparatus()
        {
            if (CookingApparatusTypeName == "oven")
                return Models.CookingApparatusType.Oven;
            if (CookingApparatusTypeName == "stove")
                return Models.CookingApparatusType.Stove;

            return null;
        }

        public Food() : base()
        {

        }
    }
}
