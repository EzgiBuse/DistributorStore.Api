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
using DistributorStore.Schema;
using MediatR.NotificationPublishers;

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
          
            if (dealer == null)
            {
                return new ApiResponse<List<Order>>("dealer not found")
                {
                    Response = null,
                  //  Message = "Dealer not found",
                    Success = false
                };
            }
            //returns the list of orders for that dealerid
            var orders = unitofwork.OrderRepository.Where(o=>o.DealerID == dealerid).ToList();




            return new ApiResponse<List<Order>>(orders);
        }

        public ApiResponse CreateOrderDealer(OrderRequest request)
        {
            var dealer = unitofwork.DealerRepository.GetById(request.DealerId);
            //returns invalid order if the order is null
            if (request == null || request.OrderItems == null || request.OrderItems.Count == 0 || request.DealerId ==null || dealer ==null)
            {
                return new ApiResponse { Success = false, Message = "Invalid order data" };
            }
            double totalOrderCost = 0;
            
            //checking if requested order amount of a product is available
            foreach (var item in request.OrderItems)
            {
                var product = unitofwork.ProductRepository.GetById(item.ProductId);

                if (product == null || product.StockQuantity < item.Quantity)
                {
                    return new ApiResponse { Success = false, Message = $"Insufficient stock for Product ID: {item.ProductId}" };
                }



                // Calculate the total order cost with the dealer profit margin
                
                totalOrderCost += (product.Price * (1 + dealer.ProfitMargin)) * item.Quantity;
            }

            // Check the dealer's balance if the payment method is Balance(2)
            if (request.PaymentMethod == 2)
            {
                if(dealer.Limit < (decimal)totalOrderCost)
                {
                    return new ApiResponse { Success = false, Message = $"Insufficient balance" };
                }
            }
            // Process payment method
            switch (request.PaymentMethod)
            {
                case 0:
                    // No action needed for EFT(0)
                    break;
                case 1:
                    // Implement credit card payment logic here(1)
                    break;
                case 2:
                    dealer.Limit = dealer.Limit - (decimal)totalOrderCost;
                    unitofwork.DealerRepository.Update(dealer);
                    break;
                default:
                    return new ApiResponse { Success = false, Message = "Invalid payment method" };
            }

            // Create the order
            var order = new Order
            {
                DealerID = request.DealerId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.WaitingforApproval, 
                PaymentMethod = (PaymentMethods)Convert.ToInt32(request.PaymentMethod),
                TotalAmount = Convert.ToDouble(totalOrderCost)
            };

            // Add the order to the database
            unitofwork.OrderRepository.Insert(order);
            unitofwork.OrderRepository.Save(); // Save changes to the database
            int orderId = order.OrderID;
            // Add order details
            foreach (var item in request.OrderItems)
            {
                var orderDetail = new OrderDetails
                {
                    OrderID = orderId,
                    ProductID = item.ProductId,
                    Quantity = item.Quantity
                };

                //updating product stock values after ordering
                var product = unitofwork.ProductRepository.GetById(item.ProductId);
                product.StockQuantity = product.StockQuantity - item.Quantity;
                unitofwork.ProductRepository.Update(product);
                unitofwork.ProductRepository.Save();
                // Add the order detail to the database
              
                unitofwork.OrderDetailRepository.Insert(orderDetail);
                unitofwork.OrderDetailRepository.Save(); // Save changes to the database
            }

            return new ApiResponse { Success = true, Message = "Order Created Successfully" };
        }
       

    }
}
