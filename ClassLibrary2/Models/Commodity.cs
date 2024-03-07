using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Commodity
    {
        public Commodity()
        {
            Orders = new HashSet<Order>();
        }

        public int CommodityId { get; set; }
        public int? CategoryId { get; set; }
        public string? CommodityName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitInStock { get; set; }

        public virtual CommodityCategory? Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
