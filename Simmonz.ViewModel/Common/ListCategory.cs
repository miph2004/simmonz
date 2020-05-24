using Simmonz.ViewModel.Category;
using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Common
{
    public class ListCategory<T>: CategoryViewModel
    {
       public List<T> Categories { get; set; }
    }
}
