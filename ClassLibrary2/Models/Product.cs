using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdCategory { get; set; }
        public double Price { get; set; }

        public virtual Category IdCategoryNavigation { get; set; } = null!;
    }
}
