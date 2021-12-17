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
        private readonly ICookService _cookService;

        private readonly ILogger<KitchenWorker> logger;

        public KitchenWorker(ICookService cookService, ILogger<KitchenWorker> logger)
        {
            _cookService = cookService;
            this.logger = logger;
        }

        protected override async  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("$Worker running ");
            await _cookService.StartWork(stoppingToken);
        }
    }
}
