using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.ProductS
{
    public interface IProductService
    {
        ApiResponse<List<Product>> ListProductsAdmin();
        ApiResponse<List<Product>> ListProductsDealer(int dealerid);
        ApiResponse UpdateProductAdmin(Product product);
        ApiResponse AddProductAdmin(Product product);
        ApiResponse DeleteProductAdmin(Product product);
    }
}
