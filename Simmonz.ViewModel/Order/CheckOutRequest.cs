using Simmonz.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Order
{
    public class CheckOutRequest : TransactionViewModel
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public int Status { get; set; }
        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
