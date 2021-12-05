using Kitchen.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kitchen.Models;

namespace Kitchen.Domain.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private KitchenContext _kitchenContext { get; }

        public BaseRepository(KitchenContext kitchenContext)
        {
            this._kitchenContext = kitchenContext;
        }

        public void InitContext()
        {
            _kitchenContext.InitContext();
        }
        //

        public List<Order> GetOrders()
        {
            return _kitchenContext.Orders;
        }

        public List<Order> GetReadyOrders()
        {
            return _kitchenContext.Orders.Where(x => x.IsReady).ToList();
        }

        public CookingApparatus GetAvailableApparatus(KitchenFood food)
        {
            if (food.CookingApparatus is null)
            {
                return null;
                
            }

            return _kitchenContext.CookingApparatuses.FirstOrDefault(ck => food.CookingApparatusType == ck.Type);
        }

        public void Prepare(KitchenFood food, CookingApparatus apparatus, Order order)
        {
            Console.WriteLine($"Cook {food.Id} started preparing food {food.Name}.");
            Thread.Sleep(food.PreparationTime * 5);
            food.State = KitchenFoodState.Ready;
            UpdateKitchenFoodState(food, order);
            
            
        }

        public KitchenFood UpdateKitchenFoodState(KitchenFood food, Order order)
        {
            _kitchenContext.Orders.FirstOrDefault(x => x.Id == order.Id)?.RealItems
                .Where(x => x.Id == food.Id)
                .ToList()
                .ForEach(x => x =food);

            return food;

        }

        public List<Food> GetMenu()
        {
            return _kitchenContext.Menu;
        }
        
        
        public List<Order> AddOrder(Order order)
        {
            var menu = GetMenu();
            order.Items.ForEach((food) => order.RealItems.Add(new KitchenFood(menu.FirstOrDefault(x => x.Id == food))));
            _kitchenContext.Orders.Add(order);

            return _kitchenContext.Orders;
        }


        public List<Cook> GetCooks()
        {
            return _kitchenContext.Cooks;
        }

        public CookingApparatus UpdateApparatus(CookingApparatus apparatus)
        {
            _kitchenContext.CookingApparatuses.Where(x => x.Id == apparatus.Id).ToList().ForEach(x => x = apparatus);
            return apparatus;
        }
    }
 }

