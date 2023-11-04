using DistributorStore.Data.ApplicationDbContext;
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
    public class UnitOfWork : IUnitOfWork
    {


        private readonly DistributorStoreDbContext dbContext;
        public UnitOfWork(DistributorStoreDbContext dbContext)
        {
            this.dbContext = dbContext;

            OrderRepository = new GenericRepository<Order>(dbContext);
            DealerRepository = new GenericRepository<Dealer>(dbContext);
           
            ProductRepository = new GenericRepository<Product>(dbContext);
            OrderDetailRepository = new GenericRepository<OrderDetails>(dbContext);
            UserRepository = new GenericRepository<User>(dbContext);
            MessageRepository = new GenericRepository<Message>(dbContext);

        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }

        public void CompleteWithTransaction()
        {
            using (var dbTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();

                }
            }
        }

        public IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : class
        {
            return new GenericRepository<Entity>(dbContext);
        }

        public IGenericRepository<Order> OrderRepository { get; private set; }
        public IGenericRepository<Dealer> DealerRepository { get; private set; }

        public IGenericRepository<Product> ProductRepository { get; private set; }

       // public IGenericRepository<OrderDetails> OrderDetailsRepository { get; private set; }
        public IGenericRepository<User> UserRepository { get; private set; }

        public IGenericRepository<OrderDetails> OrderDetailRepository { get; private set; }

        public IGenericRepository<Message> MessageRepository { get; private set; }
    }
}
