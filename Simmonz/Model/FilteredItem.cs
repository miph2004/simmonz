using Simmonz.Data.Entities;
using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Model
{
    public class FilteredItem
    {
        public int Id { get; set; }
        public List<ProductViewModel> Product { get; set; }
    }
}
