using System;
using System.Collections.Generic;

namespace ClassLibrary2.DTO
{
    public partial class OrderDTO
    {

        public int? orderId { get; set; }
        public int? commodityId {  get; set; }
        public string? commodityName { get; set; }
        public int? quantity { get; set; }
        public decimal? price { get; set; }
        public int? promotionId { get; set; }
        public string? customerName { get; set; }
        public string? address { get; set; }
        public string? phoneNumber { get; set; }
        public string? note { get; set; }
        public int? userId { get; set; }
        public string? status { get; set;}
        public string? TotalPayment { get; set; }
        public int? DateId { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
