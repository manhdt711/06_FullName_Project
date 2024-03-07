using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CommodityId { get; set; }
        public int? DateId { get; set; }
        public int? CustomerId { get; set; }
        public int? StoreId { get; set; }
        public int? PricedProducts { get; set; }
        public int? PromotionId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? UserId { get; set; }

        public virtual Commodity? Commodity { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Date? Date { get; set; }
        public virtual Store? Store { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
