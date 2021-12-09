using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Models;
using System.Threading;

namespace Kitchen.Service
{
    public interface IKitchenService
    {
        Task StartWork();
        void ReceiveOrder(Order order);
        Task StartWork(CancellationToken stoppingToken);
    }
}