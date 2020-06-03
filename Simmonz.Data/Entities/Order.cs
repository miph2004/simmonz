using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
        public int Status { get; set; }
        public int TransactionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Transaction Transaction { get; set; }
    }
}
