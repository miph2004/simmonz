using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Category
{
    public class CategoryViewModel:ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
