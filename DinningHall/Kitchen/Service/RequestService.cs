using Kitchen.Domain.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kitchen.Models;

namespace Kitchen.Service
{
    public class RequestService : IRequestService
    {
        private static string sendUrl = "http://localhost:8000/";


        public  async Task SendReadyOrder(Order order)
        {
            using var client = new HttpClient();
            var message = JsonSerializer.Serialize(order);
            var response = await client.PostAsync(sendUrl + "/ready/order",
                new StringContent(message, Encoding.UTF8, "application/json"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"Order {order.Id} was sent.");
            }
        }
    }
}
