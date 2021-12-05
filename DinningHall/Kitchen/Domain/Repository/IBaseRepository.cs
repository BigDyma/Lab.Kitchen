using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Models;

namespace Kitchen.Domain.Repository
{
    public interface IBaseRepository
    {
        List<Order> GetOrders();
        List<Order> GetReadyOrders();
        CookingApparatus GetAvailableApparatus(KitchenFood food);

        void Prepare(KitchenFood food, CookingApparatus apparatus, Order order);
        List<Food> GetMenu();
        KitchenFood UpdateKitchenFoodState(KitchenFood food, Order order);

        List<Order> AddOrder(Order order);

        List<Cook> GetCooks();
        CookingApparatus UpdateApparatus(CookingApparatus apparatus);
    }
}