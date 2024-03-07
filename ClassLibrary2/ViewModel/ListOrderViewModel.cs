using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using ElecStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ElecStore.ViewModel
{
    public class ListOrderViewModel : INotifyPropertyChanged
    {
        private readonly ElectricStore1Context _context;
        private Order _selectedOrder;
        private List<OrderDetail> _orderDetails;

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    LoadOrderDetails();
                    OnPropertyChanged(nameof(SelectedOrder));
                }
            }
        }

        public List<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                if (_orderDetails != value)
                {
                    _orderDetails = value;
                    OnPropertyChanged(nameof(OrderDetails));
                }
            }
        }

        public ListOrderViewModel(ElectricStore1Context context)
        {
            _context = context;
            LoadOrder();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.Include(x => x.OrderDetails).Include(x => x.Store)
                .Include(x => x.Date).Include(x => x.Commodity).Include(x => x.Customer).ToList();
        }

        public void LoadOrder()
        {
            var orders = GetOrders();
            // Perform any necessary filtering or sorting here
            // For example, you could order the orders by date descending:
            // orders = orders.OrderByDescending(o => o.Date).ToList();
            // You may also want to apply pagination if there are many orders

            // Chọn đơn hàng đầu tiên nếu danh sách không rỗng
            if (orders.Any())
            {
                SelectedOrder = orders.First();
            }
        }

        private void LoadOrderDetails()
        {
            if (SelectedOrder != null)
            {
                // Assuming OrderDetails is a navigation property in Order class
                OrderDetails = _context.OrderDetails.Where(od => od.OrderId == SelectedOrder.OrderId).ToList();
            }
            else
            {
                OrderDetails = new List<OrderDetail>();
            }

            // Kiểm tra OrderDetails trước khi gán
            if (OrderDetails != null)
            {
                // In ra để kiểm tra
                foreach (var detail in OrderDetails)
                {
                    Debug.WriteLine($"OrderDetail: {detail.OrderId}, {detail.UnitPrice}, {detail.Quantity}, {detail.TotalPrice}");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
