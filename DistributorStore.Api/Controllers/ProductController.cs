using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Operation.Services.OrderS;
using DistributorStore.Operation.Services.ProductS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DistributorStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //interface ile değiştir en son !!!!!!!!!!!
        private readonly IProductService service;
        public ProductController(IProductService service)
        {
            this.service = service;

        }

        [HttpGet("ListProductsDealer")]
        public ApiResponse<List<Product>> ListProductsDealer(int dealerid)
        {
            var res = service.ListProductsDealer(dealerid);
            return res;
        }

        [HttpGet("ListProductsAdmin")]
        public ApiResponse<List<Product>> ListProductsAdmin()
        {
            var res = service.ListProductsAdmin();
            return res;
        }
        [HttpPut("UpdateProductAdmin")]
        public ApiResponse UpdateProductAdmin([FromBody] Product product)
        {
            var res = service.UpdateProductAdmin(product);
            return res;
        }

        [HttpPost("AddProductAdmin")]
        public ApiResponse AddProductAdmin([FromBody] Product product)
        {
            var res = service.AddProductAdmin(product);
            return res;
        }
        [HttpDelete("DeleteProductAdmin")]
        public ApiResponse DeleteProductAdmin([FromBody] Product product)
        {
            var res = service.DeleteProductAdmin(product);
            return res;
        }
    }
}
