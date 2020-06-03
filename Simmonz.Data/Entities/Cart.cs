using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Entities
{

        public class Cart
        {
            public int Id { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
}
