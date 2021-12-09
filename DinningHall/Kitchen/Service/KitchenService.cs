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
    public class KitchenService : IKitchenService
    {
        private readonly IBaseRepository _baseRepository;

        private readonly IRequestService _server;

        private readonly ILogger<KitchenService> _logger;

        public KitchenService(IRequestService server, IBaseRepository baseRepository, ILogger<KitchenService> logger)
        {
            _server = server;
            this._baseRepository = baseRepository;
            _logger = logger;
        }
        public void ReceiveOrder(Order order)
        {
            if (order is object)
                _baseRepository.AddOrder(order);
        }

        public async Task StartWork()
        {
            

            var Cooks = await _baseRepository.GetCooks();
                var orders = await _baseRepository.GetOrders();
                await Task.Run(async () =>
                {
                    foreach (var cook in Cooks)
                    {
                        for (int i = 0; i < cook.Proficiency; i++)
                        {
                            if (orders is object)
                            {
                                //Parallel.ForEach(orders, body: (order) =>
                                //{
                                foreach (var order in orders)
                                { 
                                    if (!order.IsReady)
                                        foreach (var food in order.RealItems.Where(food => food.Complexity <= cook.Rank))
                                    {
                                        var apparatus = await _baseRepository.GetAvailableApparatus(food);
                                        if (food.State == KitchenFoodState.NotStarted && apparatus is object)
                                        {
                                            apparatus.Busy = true;
                                            await _baseRepository.UpdateApparatus(apparatus);

                                            Console.WriteLine($"Cooking apparatus was locked by {food.Name}");
                                            food.State = KitchenFoodState.Preparing;
                                            await _baseRepository.UpdateKitchenFoodState(food, order);
                                            await  _baseRepository.Prepare(food, apparatus, order);
                                            if (order.IsReady)
                                            {
                                                Console.WriteLine($"Order {order.Id} is ready ");
                                                await _server.SendReadyOrder(order).ConfigureAwait(false);
                                            }
                                        }
                                    }
                                }
                                    //}
                                //);
                            }
                        }
                    }
                });
            }
        
        public async Task StartWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await StartWork();
                await Task.Delay(1000);
            }
        }
        }

    }

