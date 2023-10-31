using DistributorStore.Data.Domain;
using DistributorStore.Data.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Data.Uow
{
    public interface IUnitOfWork
    {
        void Complete();
        void CompleteWithTransaction();

        IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : class;
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<Dealer> DealerRepository { get; }
        IGenericRepository<OrderDetails> OrderDetailRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<User> UserRepository { get; }
      



    }
}
