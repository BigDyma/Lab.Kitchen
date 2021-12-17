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

            List<Food> foods = new List<Food> {
                   new Food
                   {
                       Name = "Pizza",
                       PreparationTime = 20,
                       Complexity = 2,
                       CookingApparatusTypeName = "oven"
                   },
                new Food
                {
                    Name = "Salad",
                    PreparationTime = 10,
                    Complexity = 1,
                    CookingApparatusTypeName = null
                },
                new Food
                {
                    Name = "Zeama",
                    PreparationTime = 7,
                    Complexity = 1,
                    CookingApparatusTypeName = "stove"
                },
                new Food
                {
                    Name = "Scallop Sashami with Meyer Lemon Confit",
                    PreparationTime = 32,
                    Complexity = 3,
                    CookingApparatusTypeName = null
                },
                new Food
                {
                    Name = "Island Duck with Mulberry Mustard",
                    PreparationTime = 35,
                    Complexity = 3,
                    CookingApparatusTypeName = "oven"
                },
                new Food
                {
                    Name = "Waffles",
                    PreparationTime = 10,
                    Complexity = 1,
                    CookingApparatusTypeName = "stove"
                },
                new Food
                {
                    Name = "Aubergine",
                    PreparationTime = 20,
                    Complexity = 2,
                    CookingApparatusTypeName = null
                },
                new Food
                {
                    Name = "Lasagna",
                    PreparationTime = 30,
                    Complexity = 2,
                    CookingApparatusTypeName = "oven"
                },
                new Food
                {
                    Name = "Burger",
                    PreparationTime = 15,
                    Complexity = 1,
                    CookingApparatusTypeName = "oven"
                },
                new Food
                {
                    Name = "Gyros",
                    PreparationTime = 15,
                    Complexity = 1,
                    CookingApparatusTypeName = null
                }
            };

            return foods;
            //using (StreamReader r = new StreamReader(@"C:\Users\dumitru.strelet\Desktop\PR_labs\t\Lab.Kitchen\DinningHall\Kitchen\Utils\Menu.json"))
            //{
            //    string json = r.ReadToEnd();
            //    List<Food> items = JsonSerializer.Deserialize<List<Food>>(json);
            //    return items;
            //}
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
                    Proficiency = 2,
                    Name = "Zubenko Mikhael Petrovich",
                    CatchPhrase = "Mafioznik"
                },
                new Cook
                {
                    Rank = 2,
                    Proficiency = 2,
                    Name = "Allakh",
                    CatchPhrase = "Gde detonator ?"
                },
                new Cook
                {
                    Rank = 2,
                    Proficiency = 3,
                    Name = "San Sanych",
                    CatchPhrase = "Byli by vy chelovekom, uvazhaemyi afroamerikanets"
                },
                new Cook
                {
                    Rank = 3,
                    Proficiency = 4,
                    Name = "San Another sanych",
                    CatchPhrase = "Byli by vy chelovekom, uvazhaemyi chelovek"
                }
            };
            result.Sort((o1,o2) => o1.Proficiency - o2.Proficiency);

            return result;
        }
    }
}
