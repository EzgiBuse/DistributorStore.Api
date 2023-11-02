using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.OrderS
{
    public interface IOrderService
    {
        ApiResponse CancelOrderDealer(Order order);
        ApiResponse CancelOrderAdmin(Order order);
        ApiResponse ApproveOrderAdmin(Order o);
        ApiResponse<List<Order>> ListOrdersDealer(int dealerid);
        ApiResponse CreateOrderDealer(OrderRequest request);
    }
}
