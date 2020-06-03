using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Simmonz.ViewModel.OrderDetail
{
    public  class OrderDetailViewModel
    {
        public int Id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        [DisplayName("Tổng giá")]
        public decimal Amount { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
    }
}
