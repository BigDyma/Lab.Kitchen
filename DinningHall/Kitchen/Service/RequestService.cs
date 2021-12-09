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
using Newtonsoft.Json;

namespace Kitchen.Service
{
    public class RequestService : IRequestService
    {
        private static string sendUrl = "http://localhost:64359/";

        public  async Task SendReadyOrder(Order order)
        {
            using var client = new HttpClient();

            var res = await PostSendOrder(order, client);

            if (res.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"Order {order.Id} was sent.");
            }
        }

        private async Task<HttpResponseMessage> PostSendOrder(Order filter, HttpClient httpClient)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, $"{sendUrl}api/Order");

            var convertedJson = JsonConvert.SerializeObject(filter);
            msg.Content = new StringContent(convertedJson, Encoding.UTF8, "Application/*+json");

            var response = await httpClient.SendAsync(msg);

            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
