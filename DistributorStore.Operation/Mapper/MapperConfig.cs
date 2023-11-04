using AutoMapper;
using DistributorStore.Data.Domain;
using DistributorStore.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductStockResponse>();
            CreateMap<ProductStockResponse, Product>();


        }
    }
}
