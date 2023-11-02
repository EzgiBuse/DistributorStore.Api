using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Operation.Services.OrderS;
using DistributorStore.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributorStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;
        public OrderController(IOrderService service)
        {this.service = service;
           
        }

        [HttpPut("CancelOrderDealer")]
        public ApiResponse CancelOrderDealer([FromBody] Order order)
        {
            var res = service.CancelOrderDealer(order);
            return res;
        }

        [HttpGet("ListOrdersDealer")]
        public ApiResponse<List<Order>> ListOrdersDealer([FromBody] int dealerid)
        {
            var res = service.ListOrdersDealer(dealerid);
            return res;
        }

        [HttpPost("CreateOrderDealer")]
        public ApiResponse CreateOrderDealer([FromBody] OrderRequest orderrequest) 
        { var order = service.CreateOrderDealer(orderrequest);
            return order;
        }

        [HttpPut("ApproveOrderAdmin")]
        public ApiResponse ApproveOrderAdmin([FromBody] Order order)
        {var approve = service.ApproveOrderAdmin(order);
            return approve;

        }

        [HttpPut("CancelOrderAdmin")]
        public ApiResponse CancelOrderAdmin([FromBody] Order order)
        {
            var cancel = service.CancelOrderAdmin(order);
            return cancel;

        }

    }
}
