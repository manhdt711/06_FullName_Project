using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2.DTO
{
    public class CommodityDTO
    {
        public int? CommodityId { get; set; }
        public string? CommodityName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
        public string? CategoryName { get; set; }
    }
}
