namespace DistributorStore.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    using global::DistributorStore.Base.Response;
    using global::DistributorStore.Operation.Services.ReportS;
    using global::DistributorStore.Schema;
    using Microsoft.AspNetCore.Authorization;
    using System.Data;
    using Microsoft.AspNetCore.OutputCaching;

    namespace DistributorStore.Api.Controllers
    {
        [Authorize(Roles = "Admin, Dealer")]
        [ApiController]
        [Route("api/[controller]")]
        [ResponseCache(Duration = 600)]
        public class ReportController : ControllerBase
        {
            private readonly IReportService service;

            public ReportController(IReportService service)
            {
                this.service = service;
            }

            [Authorize(Roles = "Admin")]
            [HttpGet("GetProductStockListAdmin")]
            public ApiResponse<List<ProductStockResponse>> GetProductStockListAdmin()
            {
                var result = service.GetProductStockListAdmin();
                return result;
            }

            [Authorize(Roles = "Admin")]
            [HttpGet("GetLowStockProductList")]
            public ApiResponse<List<ProductStockResponse>> GetLowStockProductList()
            {
                var result = service.GetLowStockProductList();
                return result;
            }
           
            [Authorize(Roles = "Admin")]
            [HttpGet("GetReportsAdmin")]
            public ApiResponse<OrderReport> GetReportsAdmin(ReportRequest request)
            {
                var result = service.GetReportsAdmin(request);
                return result;
            }
        }
    }

}
