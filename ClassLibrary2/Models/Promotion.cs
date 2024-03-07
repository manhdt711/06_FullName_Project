using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Promotion
    {
        public int PromotionId { get; set; }
        public string? PromotionName { get; set; }
        public double? Discount { get; set; }
    }
}
