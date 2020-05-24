using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string AddressNumber { get; set; }
        public string AddressStreet { get; set; }
        public string AddressDistrict { get; set; }
        public float Amount { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int ShippingFeeId { get; set; }
        public int AdminId { get; set; }
        public int ClientId { get; set; }
        public Admin Admin { get; set; }
        public Client Client { get; set; }
        public ShippingFee ShippingFee { get; set; }
        
    }
}
