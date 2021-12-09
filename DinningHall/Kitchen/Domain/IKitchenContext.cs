using Kitchen.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen
{
    public interface IKitchenContext
    {
        List<CookingApparatus> CookingApparatuses { get; set; }
        List<Cook> Cooks { get; set; }
        List<Food> Menu { get; set; }
        List<Order> Orders { get; set; }

        Task InitContext();
   }
}