using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Simmonz.ViewModel.Product
{
    public class ProductCreateRequest
    {
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
        [DisplayName("Danh mục")]
        public int CategoryId { get; set; }
        [DisplayName("Giam gia ")]
        public int DiscountId { get; set; }
        [DisplayName("Hình ảnh")]
        public IFormFile Image { get; set; }

        [DisplayName("Gía")]

        public decimal Price { get; set; }

        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }
    }
}
