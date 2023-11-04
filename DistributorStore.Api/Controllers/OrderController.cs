using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Operation.Services.OrderS;
using DistributorStore.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributorStore.Api.Controllers
{
    [Authorize(Roles = "Admin, Dealer")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;
        public OrderController(IOrderService service)
        {this.service = service;
           
        }
        [Authorize(Roles = "Dealer")]
        [HttpPut("CancelOrderDealer")]
        public ApiResponse CancelOrderDealer([FromBody] Order order)
        {
            var res = service.CancelOrderDealer(order);
            return res;
        }
        [Authorize(Roles = "Dealer")]
        [HttpGet("ListOrdersDealer")]
        public ApiResponse<List<Order>> ListOrdersDealer(int dealerid)
        {
            var res = service.ListOrdersDealer(dealerid);
            return res;
        }
        [Authorize(Roles = "Dealer")]
        [HttpPost("CreateOrderDealer")]
        public ApiResponse CreateOrderDealer([FromBody] OrderRequest orderrequest) 
        { var order = service.CreateOrderDealer(orderrequest);
            return order;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ApproveOrderAdmin")]
        public ApiResponse ApproveOrderAdmin([FromBody] Order order)
        {var approve = service.ApproveOrderAdmin(order);
            return approve;

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("CancelOrderAdmin")]
        public ApiResponse CancelOrderAdmin([FromBody] Order order)
        {
            var cancel = service.CancelOrderAdmin(order);
            return cancel;

        }

    }
}
