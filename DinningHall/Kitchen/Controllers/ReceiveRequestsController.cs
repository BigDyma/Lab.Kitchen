using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kitchen.Models;
using Kitchen.Service;
using Microsoft.AspNetCore.Authorization;

namespace Kitchen.Controllers
{
    [Route("api")]
    [AllowAnonymous]
    [ApiController]
    public class ReceiveRequestsController : ControllerBase
    {
        private readonly IKitchenService _kitchenService;
        public ReceiveRequestsController(IKitchenService kitchenService)
        {
            this._kitchenService = kitchenService;
        }

        [HttpPost("Order")]
        public  async Task<IActionResult >ReceiveOrder(Order order)
        {
            if (order is null)
                return BadRequest();

            _kitchenService.ReceiveOrder(order);
            return await Task.Run(() => Ok());
        }
        
        [HttpPost("Kitchen/closed")]
        public  void Stop()
        {
            System.Environment.Exit(1);
        }
    }
}
