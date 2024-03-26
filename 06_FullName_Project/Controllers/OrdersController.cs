using ClassLibrary2.DTO;
using ClassLibrary2.ViewModel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _06_FullName_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ElectricStore1Context _context;
        public OrdersController(ElectricStore1Context context)
        {
            _context = context;
        }

        [HttpGet("GetAllOrder/{id}")]
        public ActionResult<List<ClassLibrary2.DTO.OrderDTO>> GetAllOrder(int id)
        {
            List<ClassLibrary2.DTO.OrderDTO> orders = new List<ClassLibrary2.DTO.OrderDTO>();
            if (id == 0)
            {
                orders = _context.Orders.Select(x =>
            new ClassLibrary2.DTO.OrderDTO
            {
                commodityId = x.CommodityId,
                commodityName = x.Commodity.CommodityName,
                quantity = x.OrderDetails.ToList()[0].Quantity,
                price = x.OrderDetails.ToList()[0].UnitPrice,
                customerName = x.Customer.CustomerName,
                address = x.Customer.CustomerAddress,
                phoneNumber = x.Customer.CustomerPhone,
                note = x.Customer.Comment,
                userId = x.UserId,
                status = x.PaymentMethod,
                orderId = x.OrderId,
                TotalPayment = x.InvoiceNumber,
                DateId = x.DateId,
                OrderDate = DateDAO.GetDateByOrderID(x.OrderId).OrderDate,
            }).ToList();
            }
            else
            {
                orders = _context.Orders.Where(x => x.UserId == id).Select(x =>
            new ClassLibrary2.DTO.OrderDTO
            {
                commodityId = x.CommodityId,
                commodityName = x.Commodity.CommodityName,
                quantity = x.OrderDetails.ToList()[0].Quantity,
                price = x.OrderDetails.ToList()[0].UnitPrice,
                customerName = x.Customer.CustomerName,
                promotionId = x.PromotionId,
                address = x.Customer.CustomerAddress,
                phoneNumber = x.Customer.CustomerPhone,
                note = x.Customer.Comment,
                userId = x.UserId,
                status = x.PaymentMethod,
                orderId = x.OrderId,
                TotalPayment = x.InvoiceNumber,
                DateId = x.DateId,
                OrderDate = DateDAO.GetDateByOrderID(x.OrderId).OrderDate,
            }).ToList();
            }
            return orders;
        }
        [HttpPost("AddOrder")]
        public ActionResult AddOrder([FromBody] ClassLibrary2.DTO.OrderDTO orderDTO)
        {
            //get dicount price
            double? discountPrice = 0;
            Promotion p = _context.Promotions.FirstOrDefault(x => x.PromotionId == orderDTO.promotionId);
            if (p != null) {
                discountPrice = p.Discount;
            }
            // Get the current local time
            DateTime currentTime = DateTime.Now;

            // Get the current UTC time
            DateTime currentUtcTime = DateTime.UtcNow;
            Date date = new Date()
            {
                Year = currentTime.Year,
                Month = currentTime.Month,
                Day = currentTime.Day,
            };
            //_context.Dates.Add(date);
            //_context.SaveChanges();
            List<OrderDetail> orderDetail = new List<OrderDetail>();
            orderDetail.Add(new OrderDetail()
            {
                UnitPrice = orderDTO.price,
                Quantity = orderDTO.quantity,
                TotalPrice = orderDTO.price * orderDTO.quantity
            });


            Customer customer = new Customer()
            {
                CustomerAddress = orderDTO.address,
                CustomerPhone = orderDTO.phoneNumber,
                Comment = orderDTO.note,
                CustomerName = orderDTO.customerName
            };

            //_context.Customers.Add(customer);
            //_context.SaveChanges();
            
            Order order = new Order()
            {
                CommodityId = orderDTO.commodityId,
                //CustomerId = customer.CustomerId,
                //DateId = date.DateId,
                PaymentMethod = "pending",
                PricedProducts = (int)orderDTO.price,
                Date = date,
                PromotionId = orderDTO.promotionId,
                Customer = customer,
                User = _context.Users.FirstOrDefault(x => x.UserId == orderDTO.userId),
                OrderDetails = orderDetail,
                InvoiceNumber = ((orderDTO.quantity * orderDTO.price) - (decimal)discountPrice).ToString(),
                UserId = orderDTO.userId
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPost("DeleteOrder")]
        public ActionResult DeleteOrder([FromBody] Order orderDele)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.OrderId == orderDele.OrderId);
            OrderDetail orderDetail = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderDele.OrderId);
            _context.OrderDetails.Remove(orderDetail);
            _context.SaveChanges();

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("ChangeStatusOrder")]
        public ActionResult ChangeStatusOrder([FromBody] Order orderChangeStatus)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.OrderId == orderChangeStatus.OrderId);
            order.PaymentMethod = orderChangeStatus.PaymentMethod;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("GetOrdersByDateRange/{dateFrom}/{dateTo}")]
        public ActionResult<List<OrderDTO>> GetOrdersByDateRange(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!dateFrom.HasValue && !dateTo.HasValue)
            {
                return BadRequest("At least one of dateFrom and dateTo must be provided.");
            }
            if (!dateFrom.HasValue)
            {
                List<OrderDTO> orders = _context.Orders.Select(x =>
            new ClassLibrary2.DTO.OrderDTO
            {
                commodityId = x.CommodityId,
                commodityName = x.Commodity.CommodityName,
                quantity = x.OrderDetails.ToList()[0].Quantity,
                price = x.OrderDetails.ToList()[0].UnitPrice,
                customerName = x.Customer.CustomerName,
                address = x.Customer.CustomerAddress,
                phoneNumber = x.Customer.CustomerPhone,
                note = x.Customer.Comment,
                userId = x.UserId,
                status = x.PaymentMethod,
                orderId = x.OrderId,
                TotalPayment = x.InvoiceNumber,
                DateId = x.DateId,
                OrderDate = DateDAO.GetDateByOrderID(x.OrderId).OrderDate,
            }).ToList();

                return orders.Where(x => x.OrderDate <= dateTo).ToList();
            }

            // Kiểm tra nếu dateTo là null
            if (!dateTo.HasValue)
            {
                // Lấy danh sách đơn hàng có ngày lớn hơn hoặc bằng dateFrom
                List<OrderDTO> orders = _context.Orders.Select(x =>
            new ClassLibrary2.DTO.OrderDTO
            {
                commodityId = x.CommodityId,
                commodityName = x.Commodity.CommodityName,
                quantity = x.OrderDetails.ToList()[0].Quantity,
                price = x.OrderDetails.ToList()[0].UnitPrice,
                customerName = x.Customer.CustomerName,
                address = x.Customer.CustomerAddress,
                phoneNumber = x.Customer.CustomerPhone,
                note = x.Customer.Comment,
                userId = x.UserId,
                status = x.PaymentMethod,
                orderId = x.OrderId,
                TotalPayment = x.InvoiceNumber,
                DateId = x.DateId,
                OrderDate = DateDAO.GetDateByOrderID(x.OrderId).OrderDate,
            }).ToList();

                return orders.Where(x => x.OrderDate >= dateFrom).ToList();
            }

            if (dateFrom >= dateTo)
            {
                return BadRequest("Invalid date range.");
            }

            List<OrderDTO> ordersInRange = _context.Orders.Select(x =>
            new ClassLibrary2.DTO.OrderDTO
            {
                commodityId = x.CommodityId,
                commodityName = x.Commodity.CommodityName,
                quantity = x.OrderDetails.ToList()[0].Quantity,
                price = x.OrderDetails.ToList()[0].UnitPrice,
                customerName = x.Customer.CustomerName,
                address = x.Customer.CustomerAddress,
                phoneNumber = x.Customer.CustomerPhone,
                note = x.Customer.Comment,
                userId = x.UserId,
                status = x.PaymentMethod,
                orderId = x.OrderId,
                TotalPayment = x.InvoiceNumber,
                DateId = x.DateId,
                OrderDate = DateDAO.GetDateByOrderID(x.OrderId).OrderDate,
            }).ToList();

            ordersInRange = ordersInRange.Where(x => x.OrderDate >= dateFrom && x.OrderDate <= dateTo).ToList();
            if (ordersInRange.Count == 0)
            {
                return NotFound("No orders found in the specified date range.");
            }
            return ordersInRange;
        }

    }
}
