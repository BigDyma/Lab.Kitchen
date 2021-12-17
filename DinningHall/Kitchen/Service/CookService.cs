using Kitchen.Utils;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kitchen.Domain.Repository;
using Kitchen.Models;
using Microsoft.Extensions.Logging;

namespace Kitchen.Service
{
    public class CookService : ICookService
    {
        private readonly IBaseRepository _baseRepository;

        private readonly IRequestService _server;

        private readonly ILogger<CookService> _logger;

        private readonly SemaphoreLocker _locker = new SemaphoreLocker();

        public CookService(IRequestService server, IBaseRepository baseRepository, ILogger<CookService> logger)
        {
            _server = server;
            this._baseRepository = baseRepository;
            _logger = logger;
        }

        private async Task StartWork()
        {
            var Cooks = await _baseRepository.GetCooks();
            await Task.Run(async () =>
            {
                Cooks.AsParallel().ForAll(cook =>
                {
                    SetCookToWork(cook).ConfigureAwait(false).GetAwaiter().GetResult();
                });
            });
        }

        private async Task SetCookToWork(Cook cook)
        {
            Thread.Sleep(100);
            for (int i = 0; i < cook.Proficiency; i++)
            {
                var orders = await _baseRepository.GetNotReadyOrders();

                for (var index = 0; index < orders.Count; index++)
                {
                    var order = orders[index];
                    await GetOrderRealItems(cook, order);
                }
            }
        }

        private async Task GetOrderRealItems(Cook cook, Order order)
        {
            var availableOrdersByCookRank = order.RealItems.ToList();

            for (var index = 0; index < availableOrdersByCookRank.Count; index++)
            {
                if (availableOrdersByCookRank[index].Complexity <= cook.Rank)
                {
                    var food = availableOrdersByCookRank[index];
                    Console.WriteLine($"Cook {cook.Name} is looking to {food}");

                    Console.WriteLine($"Cook {cook.Name} trying to find apparatus for {food.Name}");
                    var apparatus = await FindAvailableCookingApparatus(food, order);

                    if (apparatus.IsAvailable)
                    {
                        await CookPrepareFood(cook, apparatus.CookingApparatus, food, order);
                    }
                }
               
            }
        }

        private async Task<CookingApparatusDto> FindAvailableCookingApparatus(KitchenFood food, Order order)
        {
            return await _locker.LockAsync(async () =>
            {
                if (food.CookingApparatusType == null)
                {
                    return new CookingApparatusDto
                    {
                        CookingApparatus = null,
                        IsAvailable = true
                    };
                }
                var apparatus = await _baseRepository.GetAvailableApparatus(food);

                var wasAvailable = (food.State == KitchenFoodState.NotStarted && apparatus is object);

                if (wasAvailable)
                {
                    apparatus.Busy = true;
                    await _baseRepository.UpdateApparatus(apparatus);

                    Console.WriteLine($"Cooking apparatus was locked by {food.Name}");
                    food.State = KitchenFoodState.Preparing;
                    await _baseRepository.UpdateKitchenFoodState(food, order);
                }

                return new CookingApparatusDto
                {
                    IsAvailable = wasAvailable,
                    CookingApparatus = apparatus
                };

            });
        }
        
        private async Task CookPrepareFood(Cook cook, CookingApparatus apparatus, KitchenFood food, Order order)
        {
            await _baseRepository.Prepare(food, apparatus, order, cook);
            if (order.IsReady)
            {
                Console.WriteLine($"Order {order.Id} is ready ");
                await _server.SendReadyOrder(order);
            }
        }


        public async Task StartWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Cooks are finished");
                await StartWork();
                await Task.Delay(10000);
            }
        }
    }
}