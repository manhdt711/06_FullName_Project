using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
        }

        public int StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
