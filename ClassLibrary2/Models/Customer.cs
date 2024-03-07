using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalBought { get; set; }
        public string? Comment { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
