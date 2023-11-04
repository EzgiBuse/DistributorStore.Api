using AutoMapper;
using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Data.Uow;
using DistributorStore.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.ReportS
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;
        public ReportService(IUnitOfWork unitofwork,IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }


        public ApiResponse<List<ProductStockResponse>> GetProductStockListAdmin()
        {

            //Gets all products from database, returns an error if products are null
            var products = unitofwork.ProductRepository.GetAll();
            if (products == null)
            {
                return new ApiResponse<List<ProductStockResponse>>("No product available")
                {
                    Response = null,

                    Success = false
                };
            }

            var mapped = mapper.Map<List<ProductStockResponse>>(products);

            return new ApiResponse<List<ProductStockResponse>>(mapped);
        }
        public ApiResponse<List<ProductStockResponse>> GetLowStockProductList()
        {

            //Gets all products from database, returns an error if products are null
            var products = unitofwork.ProductRepository.GetAll();
            if (products == null)
            {
                return new ApiResponse<List<ProductStockResponse>>("No product available")
                {
                    Response = null,

                    Success = false
                };
            }
            //if product stock is lower than minimum stock, it is added to the low stock list
            List<Product> lowStockList = new List<Product>();
            foreach(var product in products)
            {
                if(product.StockQuantity < product.MinimumStock)
                {
                    lowStockList.Add(product);
                }

            }
            var mapped = mapper.Map<List<ProductStockResponse>>(lowStockList);

            return new ApiResponse<List<ProductStockResponse>>(mapped);
        }

        public ApiResponse<OrderReport> GetReportsAdmin(ReportRequest request)
        {
            
                OrderReport report = new OrderReport();
                List<Order> orders = new List<Order>();

                if (request.Dealerid.HasValue && request.ReportWindow != null)
                {
                if (request.ReportWindow == ReportWindow.Daily)
                {
                    // Fetch daily reports
                    var today = DateTime.Today;
                    orders = unitofwork.OrderRepository.Where(k => k.DealerID == request.Dealerid && k.OrderDate.Date == today).ToList();
                }
                if (request.ReportWindow == ReportWindow.Weekly)
                {
                    // Fetch weekly reports for the previous week
                    var today = DateTime.Today;
                    var previousWeekStart = today.AddDays(-(int)today.DayOfWeek - 6);
                    var previousWeekEnd = today.AddDays(-(int)today.DayOfWeek);
                    orders = unitofwork.OrderRepository.Where(k => k.DealerID == request.Dealerid && k.OrderDate.Date >= previousWeekStart && k.OrderDate.Date <= previousWeekEnd).ToList();
                }
                if (request.ReportWindow == ReportWindow.Monthly)
                {
                    // Fetch monthly reports for the previous month
                    var today = DateTime.Today;
                    var previousMonthStart = new DateTime(today.Year, today.Month - 1, 1);
                    var previousMonthEnd = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    orders = unitofwork.OrderRepository.Where(k => k.DealerID == request.Dealerid && k.OrderDate.Date >= previousMonthStart && k.OrderDate.Date <= previousMonthEnd).ToList();
                }


                 }
                      else if (request.Dealerid ==null && request.ReportWindow != null)
                 {
                // Fetch all orders
                if (request.ReportWindow == ReportWindow.Daily)
                {
                    // Fetch daily reports
                    var today = DateTime.Today;
                    orders = unitofwork.OrderRepository.Where(k => k.OrderDate.Date == today).ToList();
                }
                if (request.ReportWindow == ReportWindow.Weekly)
                {
                    // Fetch weekly reports for the previous week
                    var today = DateTime.Today;
                    var previousWeekStart = today.AddDays(-(int)today.DayOfWeek - 6);
                    var previousWeekEnd = today.AddDays(-(int)today.DayOfWeek);
                    orders = unitofwork.OrderRepository.Where(k => k.OrderDate.Date >= previousWeekStart && k.OrderDate.Date <= previousWeekEnd).ToList();
                }
                if (request.ReportWindow == ReportWindow.Monthly)
                {
                    // Fetch monthly reports for the previous month
                    var today = DateTime.Today;
                    var previousMonthStart = new DateTime(today.Year, today.Month - 1, 1);
                    var previousMonthEnd = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    orders = unitofwork.OrderRepository.Where(k =>  k.OrderDate.Date >= previousMonthStart && k.OrderDate.Date <= previousMonthEnd).ToList();
                }
                   }
            else if(request.Dealerid != null && request.ReportWindow == null)
            {
                orders = unitofwork.OrderRepository.Where(k => k.DealerID == request.Dealerid).ToList();
            }
            else if (request.Dealerid == null && request.ReportWindow == null)
            {
                orders = unitofwork.OrderRepository.GetAll();
            }

            // Process the orders and calculate the total expenditure
            List<DetailedOrder> detailedOrders = new List<DetailedOrder>();
                double totalExpenditure = 0;

                foreach (var order in orders)
                {
                    DetailedOrder detailedOrder = new DetailedOrder
                    {//fetching orderdetails of orders from the database
                        order = order,
                        Details = unitofwork.OrderDetailRepository.Where(o => o.OrderID == order.OrderID).ToList()
                    };
                    detailedOrders.Add(detailedOrder);

                    totalExpenditure += order.TotalAmount; //  for the total order amount
                }

                report.orders = detailedOrders;
                report.TotalExpenditure = totalExpenditure;

                return new ApiResponse <OrderReport>(report);
            
        }

       
    }
}
