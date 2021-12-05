using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Models;

namespace Kitchen.Service
{
    public interface IKitchenService
    {
        Task StartWork();
        void ReceiveOrder(Order order);
    }
}