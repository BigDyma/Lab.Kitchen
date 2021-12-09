using Kitchen.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DinningHall.Service
{
    public class KitchenWorker : BackgroundService
    {
        private readonly IKitchenService _kitchenService;
        private readonly ILogger<KitchenWorker> logger;

        public KitchenWorker(IKitchenService kitchenService, ILogger<KitchenWorker> logger)
        {
            _kitchenService = kitchenService;
            this.logger = logger;
        }

        protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("$Worker running ");
            await _kitchenService.StartWork(stoppingToken);
        }
    }
}
