using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kitchen.Models;
using Kitchen.Utils;

namespace Kitchen
{
    public class KitchenContext : BackgroundService
    {
        public List<Food> Menu { get; set; }
        public List<Order> Orders { get; set; }
        
        public List<Cook> Cooks { get; set; }
        
        public List<CookingApparatus> CookingApparatuses { get; set; }
        

        public KitchenContext()
        {
            InitMenu();
            InitContext();
        }

        public void InitContext()
        {
            InitCooks();
            InitCookingApparatus();
        }
        
        public void InitMenu()
        {
            Menu = KitchenUtils.getMenu();
        }
        protected void InitCookingApparatus()
        {
            CookingApparatuses = KitchenUtils.GetCookingApparatus();
        }
        protected void InitCooks()
        {
            Cooks = KitchenUtils.GetCooks();
        }

      
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitCookingApparatus();
            InitCooks();
            
            return Task.CompletedTask;
        }
    }
}
