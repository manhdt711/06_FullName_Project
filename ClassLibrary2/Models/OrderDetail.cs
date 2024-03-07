using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Order? Order { get; set; }
    }
}
