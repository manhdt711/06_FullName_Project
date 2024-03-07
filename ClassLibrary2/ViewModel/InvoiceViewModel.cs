using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ElecStore.ViewModel
{
    public class InvoiceViewModel : ViewModelBase
    {
        private int _quantity;
        private double _totalPrice;
        private double _discount;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    RaisePropertyChanged(nameof(Quantity));
                    UpdateInvoice();
                }
            }
        }

        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    RaisePropertyChanged(nameof(TotalPrice));
                    UpdateInvoice();
                }
            }
        }

        public double Discount
        {
            get { return _discount; }
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    RaisePropertyChanged(nameof(Discount));
                    UpdateInvoice();
                }
            }
        }

        public ICommand PrintInvoiceCommand { get; }

        public InvoiceViewModel()
        {
            PrintInvoiceCommand = new RelayCommand(PrintInvoice);
        }

        private void UpdateInvoice()
        {
            // Logic tính toán tổng hóa đơn sau mỗi cập nhật Quantity, TotalPrice, Discount
        }

        private void PrintInvoice()
        {
            // Logic in hóa đơn
        }
    }
}
