using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Models;

namespace Kitchen.Domain.Repository
{
    public interface IBaseRepository
    {
        Task<List<Order>> GetOrders();
        Task<List<Order>> GetReadyOrders();
        Task<CookingApparatus> GetAvailableApparatus(KitchenFood food);

        Task Prepare(KitchenFood food, CookingApparatus apparatus, Order order, Cook cook);
        Task<List<Food>> GetMenu();
        Task<KitchenFood> UpdateKitchenFoodState(KitchenFood food, Order order);

        Task<List<Order>> AddOrder(Order order);

        Task<List<Cook>> GetCooks();
        Task<CookingApparatus> UpdateApparatus(CookingApparatus apparatus);
        Task InitContext();
        Task<List<Order>> GetNotReadyOrders();
    }
}