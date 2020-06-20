using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Transaction
{
   public class TransactionViewModel
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string AddressNumber { get; set; }
        public string AddressStreet { get; set; }
        public string AddressDistrict { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int ShippingFeeId { get; set; }
        public string Admin { get; set; }
        public string Client { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
