using ElecStore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElecStore.ViewModel
{
    public class CartViewModel
    {
        public ObservableCollection<Cart> CartItems { get; set; }
        public decimal TotalAmount { get; set; }

        public CartViewModel(ElectricStore1Context context)
        {
            CartItems = new ObservableCollection<Cart>(context.Carts);
            TotalAmount = CalculateTotalAmount();
        }

        private decimal CalculateTotalAmount()
        {
            decimal total = CartItems.Sum(item => (decimal)(item.UnitPrice ?? 0) * (decimal)(item.Quantity ?? 0));
            return total;
        }
    }
}
