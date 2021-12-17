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

        private SemaphoreLocker _locker = new SemaphoreLocker();

        public BaseRepository(KitchenContext kitchenContext)
        {
            this._kitchenContext = kitchenContext;
        }

        public async Task InitContext()
        {
             await _kitchenContext.InitContext();
        }

        public async  Task<List<Order>> GetNotReadyOrders()
        {
            var orders = await GetOrders().ConfigureAwait(false);
            return orders.Where(x => x.IsReady == false).ToList();
        }

        public async Task<List<Order>> GetOrders()
        {
            return  await Task.FromResult(_kitchenContext.Orders);
        }

        public async Task<List<Order>> GetReadyOrders()
        {

            var allOrders = await GetOrders();

            return allOrders.Where(x => x.IsReady).ToList();
        }

        public async Task<CookingApparatus> GetAvailableApparatus(KitchenFood food)
        {
            var allApparatuses = await GetAllCoookingApparatus();

            return allApparatuses.FirstOrDefault(ck => food.CookingApparatusType == ck.Type);
        }

        public async Task<List<CookingApparatus>> GetAllCoookingApparatus()
        {
         //   return await _locker.LockAsync(() => 
         return await Task.FromResult(_kitchenContext.CookingApparatuses);
         //);
        }
        public async Task Prepare(KitchenFood food, CookingApparatus apparatus, Order order, Cook cook)
        {
            Console.WriteLine($"Cook {cook.Name}  started preparing food {food.Name}.");
            Thread.Sleep(food.PreparationTime * 5);
            food.State = KitchenFoodState.Ready;
            await UpdateKitchenFoodState(food, order);
            
        }

        public async Task<KitchenFood> UpdateKitchenFoodState(KitchenFood food, Order order)
        {

                await Task.Run(() =>
                {
                    _kitchenContext.Orders.FirstOrDefault(x => x.Id == order.Id)?.RealItems
                        .Where(x => x.Id == food.Id)
                        .ToList()
                        .ForEach(x => x = food);
                    return Task.CompletedTask;
                });
                return food;
        }

        public async Task<List<Food>> GetMenu()
        {
           // return await _locker.LockAsync(() =>
            // {
                return await Task.FromResult(_kitchenContext.Menu);
            // });
        }
        
        
        public async Task<List<Order>> AddOrder(Order order)
        {
            var menu = await GetMenu();

                foreach (var food in order.Items)
                {
                    var kitchenFood = new KitchenFood(menu.FirstOrDefault(x => x.Name == food.Name));
                    order.RealItems.Add(kitchenFood);
                }

               await AddOrderInternal(order);

            return await GetOrders();
        }

        private   Task AddOrderInternal(Order order)
        {
            // await _locker.LockAsync(async () =>
            _kitchenContext.Orders.Add(order);

            return  Task.CompletedTask;
            // );
        }


        public async Task<List<Cook>> GetCooks()
        {
           // return await _locker.LockAsync(async () =>
          return await Task.FromResult(_kitchenContext.Cooks);
           //);
        }

        public async Task<CookingApparatus> UpdateApparatus(CookingApparatus apparatus)
        {

           // await _locker.LockAsync (async () => 
           await Task.Run(() =>
               _kitchenContext.CookingApparatuses.Where(x => x.Id == apparatus.Id).ToList()
                   .ForEach(x => x = apparatus));
                //);
            return await Task.FromResult(apparatus);
        }
    }
 }

