using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Operation.Services.OrderS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributorStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly IOrderService service;
        public DealerController(IOrderService service)
        {this.service = service;
           
        }

        [HttpPut("CancelOrderDealer")]
        public ApiResponse CancelOrderDealer([FromBody] Order order)
        {
            var res = service.CancelOrderDealer(order);
            return res;
        }

        [HttpGet("ListOrdersDealer")]
        public ApiResponse<List<Order>> ListOrdersDealer(int dealerid)
        {
            var res = service.ListOrdersDealer(dealerid);
            return res;
        }



    }
}
