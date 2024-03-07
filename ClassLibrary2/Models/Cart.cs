using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? CommodityId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
