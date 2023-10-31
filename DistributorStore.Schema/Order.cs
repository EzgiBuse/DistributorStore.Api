using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Schema
{
    public class OrderItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
    }

    public class OrderRequest
    {
        
        public List<OrderItemRequest> OrderItems { get; set; }
       
    }
}
