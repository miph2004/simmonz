using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set;}
    }
}
