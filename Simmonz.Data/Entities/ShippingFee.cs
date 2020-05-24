using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Entities
{
    public class ShippingFee
    {
        public int Id { get; set; }
        public string District { get; set; }
        public float Fee { get; set; }
    }
}
