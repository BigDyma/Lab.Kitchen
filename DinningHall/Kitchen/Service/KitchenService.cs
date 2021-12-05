using Kitchen.Utils;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kitchen.Domain.Repository;
using Kitchen.Models;

namespace Kitchen.Service
{

    public class KitchenService : IKitchenService
    {
        private readonly IBaseRepository _baseRepository;

        private readonly IRequestService _server;

        private static Mutex mutex = new Mutex();

        public KitchenService(IRequestService server, IBaseRepository baseRepository)
        {
            _server = server;
            this._baseRepository = baseRepository;
            StartWork().GetAwaiter().GetResult();
        }
        public void ReceiveOrder(Order order)
        {
            if (order is object)
                _baseRepository.AddOrder(order);
        }

        public async Task StartWork()
        {
            for (;;)
            {
                var Cooks = _baseRepository.GetCooks();
                var orders = _baseRepository.GetReadyOrders();
                await Task.Run(() =>
                {
                    foreach (var cook in Cooks)
                    {
                        for (int i = 0; i < cook.Proficiency; i++)
                        {
                            Thread.Sleep(100);

                            Parallel.ForEach(orders, body: (order) =>
                            {
                                foreach (var food in order.RealItems.Where(food => food.Complexity <= cook.Rank))
                                {
                                    mutex.WaitOne();
                                    var apparatus = _baseRepository.GetAvailableApparatus(food);
                                    if (food.State == KitchenFoodState.NotStarted && apparatus is object)
                                    {
                                        apparatus.Busy = true;
                                        _baseRepository.UpdateApparatus(apparatus);

                                        Console.WriteLine($"Cooking apparatus was locked by {food.Name}");
                                        food.State = KitchenFoodState.Preparing;
                                        _baseRepository.UpdateKitchenFoodState(food, order);
                                        mutex.ReleaseMutex();
                                        _baseRepository.Prepare(food, apparatus, order);
                                        if (order.IsReady)
                                        {
                                            Console.WriteLine($"Order {order.Id} is ready ");
                                            _server.SendReadyOrder(order).GetAwaiter().GetResult();
                                        }
                                    }
                                    else
                                        mutex.ReleaseMutex();
                                }
                            });
                        }
                    }
                });
            }
        }
    }
}
