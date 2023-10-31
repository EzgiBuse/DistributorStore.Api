using DistributorStore.Base.Response;
using DistributorStore.Data.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributorStore.Data;
using Microsoft.AspNetCore.Mvc;
using DistributorStore.Data.Domain;
using Azure;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace DistributorStore.Operation.Services.OrderS
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitofwork;
        public OrderService(IUnitOfWork unitofwork) {
        this.unitofwork = unitofwork;
        }

        public ApiResponse CancelOrderDealer(Order o)
        {//cancels an order upon dealer's request
            var order = unitofwork.OrderRepository.GetById(o.OrderID);
            if (order == null)
            {
                return new ApiResponse("order not found");
            }
            if(order.Status == OrderStatus.Approved)
            {
                return new ApiResponse("Order has already been approved,cancellation is not possible");
            }
            if (order.Status == OrderStatus.Cancelled)
            {
                return new ApiResponse("Order has already been cancelled");
            }
            order.Status = OrderStatus.Cancelled;
            unitofwork.OrderRepository.Update(order);
            unitofwork.OrderRepository.Save();
            return new ApiResponse();
            
        }

        public ApiResponse CancelOrderAdmin(Order o)
        {//cancels an order upon admin's request
            var order = unitofwork.OrderRepository.GetById(o.OrderID);
            if (order == null)
            {
                return new ApiResponse("order not found");
            }
            if (order.Status == OrderStatus.Approved)
            {
                return new ApiResponse("Order has already been approved,cancellation is not possible");
            }
            if (order.Status == OrderStatus.Cancelled)
            {
                return new ApiResponse("Order has already been cancelled");
            }
            order.Status = OrderStatus.Cancelled;
            unitofwork.OrderRepository.Update(order);
            unitofwork.OrderRepository.Save();
            return new ApiResponse();

        }

        public ApiResponse ApproveOrderAdmin(Order o)
        {//approves an order upon admin's request
            var order = unitofwork.OrderRepository.GetById(o.OrderID);
            if (order == null)
            {
                return new ApiResponse("order not found");
            }
            if (order.Status == OrderStatus.Approved)
            {
                return new ApiResponse("Order has already been approved");
            }
            if (order.Status == OrderStatus.Cancelled)
            {
                return new ApiResponse("Order has been cancelled");
            }
            order.Status = OrderStatus.Approved;
            unitofwork.OrderRepository.Update(order);
            unitofwork.OrderRepository.Save();
            return new ApiResponse();

        }

        public ApiResponse<List<Order>> ListOrdersDealer(int dealerid)
        {
            Dealer dealer = unitofwork.DynamicRepository<Dealer>().GetById(dealerid);
            if(dealer == null)
            {
                return new ApiResponse<List<Order>>("dealer not found")
                {
                    Response = null,
                  //  Message = "Dealer not found",
                    Success = false
                };
            }
            //retorns the list of orders for that dealerid
            var orders = unitofwork.OrderRepository.Where(o=>o.DealerID == dealerid).ToList();




            return new ApiResponse<List<Order>>(orders);
        }

    }
}
