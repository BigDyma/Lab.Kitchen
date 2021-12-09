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
    public class KitchenContext : IKitchenContext
    {
        public List<Food> Menu { get; set; }
        public List<Order> Orders { get; set; }

        public List<Cook> Cooks { get; set; }

        public List<CookingApparatus> CookingApparatuses { get; set; }


        public KitchenContext()
        {
            InitMenu();
            InitOrders();
            InitContext();
        }
        public void InitOrders()
        {
            Orders = new List<Order>();
        }
        public Task InitContext()
        {
            InitCooks();
            InitCookingApparatus();
            return Task.CompletedTask;
        }

        protected void InitMenu()
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
    }
}
