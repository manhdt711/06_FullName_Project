using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Date
    {
        public Date()
        {
            Orders = new HashSet<Order>();
        }

        public int DateId { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
