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

        public async Task InitContext()
        {
             await _kitchenContext.InitContext();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await Task.FromResult(_kitchenContext.Orders);
        }

        public async Task<List<Order>> GetReadyOrders()
        {
           return await Task.Run(() => _kitchenContext.Orders?.Where(x => x.IsReady).ToList());
        }

        public async Task<CookingApparatus> GetAvailableApparatus(KitchenFood food)
        {
            return await Task.Run(() => _kitchenContext.CookingApparatuses?.FirstOrDefault(ck => food.CookingApparatusType == ck.Type));
        }

        public async Task Prepare(KitchenFood food, CookingApparatus apparatus, Order order)
        {
            Console.WriteLine($"Cook {food.Id} started preparing food {food.Name}.");
            Thread.Sleep(food.PreparationTime * 5);
            food.State = KitchenFoodState.Ready;
            await UpdateKitchenFoodState(food, order);
            
        }

        public async Task<KitchenFood> UpdateKitchenFoodState(KitchenFood food, Order order)
        {
            _kitchenContext.Orders.FirstOrDefault(x => x.Id == order.Id)?.RealItems
                .Where(x => x.Id == food.Id)
                .ToList()
                .ForEach(x => x =food);

            return  await Task.FromResult(food);

        }

        public Task<List<Food>> GetMenu()
        {
            return Task.FromResult(_kitchenContext.Menu);
        }
        
        
        public async Task<List<Order>> AddOrder(Order order)
        {

                var menu = await GetMenu();

                foreach (var food in order.Items)
                {
                    var kitchenFood = new KitchenFood(menu.FirstOrDefault(x => x.Name == food.Name));
                    order.RealItems.Add(kitchenFood);
                }

                _kitchenContext.Orders.Add(order);

            return await Task.FromResult(_kitchenContext.Orders);
        }


        public async Task<List<Cook>> GetCooks()
        {
            return await Task.FromResult(_kitchenContext.Cooks);
        }

        public async Task<CookingApparatus> UpdateApparatus(CookingApparatus apparatus)
        {
            await Task.Run (() => _kitchenContext.CookingApparatuses.Where(x => x.Id == apparatus.Id).ToList().ForEach(x => x = apparatus));
            return await Task.FromResult(apparatus);
        }
    }
 }

