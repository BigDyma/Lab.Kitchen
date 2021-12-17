using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kitchen.Models;
using System.Threading;

namespace Kitchen.Service
{
    public interface ICookService
    {
        Task StartWork(CancellationToken stoppingToken);
    }
}