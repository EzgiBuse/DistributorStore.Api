using DistributorStore.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Schema
{
    public class ReportRequest
    {
        public int? Dealerid { get; set; }
       public ReportWindow? ReportWindow { get; set; }



    }
    public class ProductStockResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }


    }
    public class OrderReport
    {
        public List<DetailedOrder> orders { get; set; }
        public double TotalExpenditure { get; set; }
    }
   
}
