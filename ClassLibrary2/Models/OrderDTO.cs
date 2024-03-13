using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class OrderDTO
    {
        public string? commodityName { get; set; }
        public int? quantity { get; set; }
        public int? price { get; set; }
        public string? customerName { get; set; }
        public string? address { get; set; }
        public string? phoneNumber { get; set; }
        public string? note { get; set; }
    }
}
