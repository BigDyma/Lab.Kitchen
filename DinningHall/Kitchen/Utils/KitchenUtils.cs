using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.Service;
using Kitchen.Models;

namespace Kitchen.Utils
{
    public abstract class KitchenUtils
    {
        public static List<Food> getMenu()
        {
            return LoadJson();
        }
        public static List<Food> LoadJson()
        {
            using (StreamReader r = new StreamReader("Menu.json"))
            {
                string json = r.ReadToEnd();
                List<Food> items = JsonSerializer.Deserialize<List<Food>>(json);
                return items;
            }
        }


        public static int GeneratePriority(Order order)
        {
            var priority = 1;
            if ((new int[] { 1, 4 }).ToList().Contains(order.Items.Count))
                priority += 2;

            if (order.MaxWaitTime <= 30)
                priority += 1;

            return priority;
        }

        public static List<CookingApparatus> GetCookingApparatus()
        {
            return new List<CookingApparatus>
            {
                new CookingApparatus {Type = CookingApparatusType.Oven},
                new CookingApparatus {Type = CookingApparatusType.Oven},
                new CookingApparatus {Type = CookingApparatusType.Stove}
            };
        }

        public static List<Cook> GetCooks()
        {
            var result = new List<Cook>
            {
                new Cook
                {

                    Rank = 1,
                    Proficiency = 1,
                    Name = "Zubenko Mikhael Petrovich",
                    CatchPhrase = "Mafioznik"
                },
                new Cook
                {
                    Rank = 2,
                    Proficiency = 3,
                    Name = "Allakh",
                    CatchPhrase = "Gde detonator ?"
                },
                new Cook
                {
                    Rank = 3,
                    Proficiency = 4,
                    Name = "San Sanych",
                    CatchPhrase = "Byli by vy chelovekom, uvazhaemyi afroamerikanets"
                }
            };
            result.Sort((o1,o2) => o1.Proficiency - o2.Proficiency);

            return result;
        }
    }
}
