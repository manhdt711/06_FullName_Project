using ElecStore.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAllOrder")]
        public ActionResult<List<ClassLibrary2.DTO.OrderDTO>> GetAllOrder()
        {
            List<ClassLibrary2.DTO.OrderDTO> orders = _context.Orders.Select(x => 
            new ClassLibrary2.DTO.OrderDTO {
                commodityId = x.CommodityId, 
                commodityName = x.Commodity.CommodityName, 
                //quantity = x.OrderDetails.ToList()[0].Quantity, 
                //price = x.OrderDetails.ToList()[0].UnitPrice,
                customerName = x.Customer.CustomerName, 
                address = x.Customer.CustomerAddress, 
                phoneNumber = x.Customer.CustomerPhone, 
                note = x.Customer.Comment,
                userId = x.UserId,
                status = x.PaymentMethod,
                orderId = x.OrderId,
            }).ToList();
            return orders;
        }
        [HttpPost("AddOrder")]
        public ActionResult AddOrder([FromBody] ClassLibrary2.DTO.OrderDTO orderDTO)
        {
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
                Customer = customer,
                OrderDetails = orderDetail,
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
    }
}
