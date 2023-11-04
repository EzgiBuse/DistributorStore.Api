using DistributorStore.Base.Response;
using DistributorStore.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.ReportS
{
    public interface IReportService
    {
        ApiResponse<List<ProductStockResponse>> GetProductStockListAdmin();
         ApiResponse<List<ProductStockResponse>> GetLowStockProductList();
        ApiResponse<OrderReport> GetReportsAdmin(ReportRequest request);
       // ApiResponse<OrderReport> GetReportsDealer(ReportRequest request);
    }
}
