using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Data.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.ProductS
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitofwork;
        public ProductService(IUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        public ApiResponse<List<Product>> ListProductsDealer(int dealerid)
        {
            Dealer dealer = unitofwork.DealerRepository.GetById(dealerid);

            if (dealer == null)
            {
                return new ApiResponse<List<Product>>("dealer not found")
                {
                    Response = null,
                    
                    Success = false
                };
            }
           
            var products = unitofwork.ProductRepository.GetAll();
            if(products == null)
            {
                return new ApiResponse<List<Product>>("No product available")
                {
                    Response = null,
                   
                    Success = false
                };
            }
            var productlist = products;
            //updating product prices according to the dealer profit margin
            List<Product> result = new List<Product>();
            foreach(var product in productlist)
            {
                product.Price = product.Price * (1 + dealer.ProfitMargin);
                result.Add(product);
            }


            return new ApiResponse<List<Product>>(result);
        }

        public ApiResponse<List<Product>> ListProductsAdmin()
        {
            
           
            var products = unitofwork.ProductRepository.GetAll();
            if (products == null)
            {
                return new ApiResponse<List<Product>>("No product available")
                {
                    Response = null,

                    Success = false
                };
            }
           


            return new ApiResponse<List<Product>>(products);
        }
        public ApiResponse UpdateProductAdmin(Product product)
        {
            var existingProduct = unitofwork.ProductRepository.GetById(product.ProductID);

            if (existingProduct == null)
            {
                return new ApiResponse("product not found");
            }

            // Update the existing product properties based on the provided data
            existingProduct.ProductName = product.ProductName;
            existingProduct.MinimumStock = product.MinimumStock;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            

            // Save the changes to the database
            unitofwork.ProductRepository.Update(existingProduct);
            unitofwork.ProductRepository.Save();

            return new ApiResponse();
        }

        public ApiResponse AddProductAdmin(Product product)
        {
            if(product == null)
            {
                return new ApiResponse("product is null");
            }
            unitofwork.ProductRepository.Insert(product);
            unitofwork.ProductRepository.Save();
            return new ApiResponse();
        }

        public ApiResponse DeleteProductAdmin(Product product)
        {
            var pr = unitofwork.ProductRepository.GetById(product.ProductID);
            if (pr == null)
            {
                return new ApiResponse("product not found");
            }
            unitofwork.ProductRepository.DeleteById(pr.ProductID);
            unitofwork.ProductRepository.Save();
            return new ApiResponse();
        }
    }

}
