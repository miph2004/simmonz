using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Simmonz.ViewModel.Product
{
    public class ProductCreateRequest
    {
        [DisplayName("Tên sản phẩm")]
        [Required]
        public string ProductName { get; set; }
        [DisplayName("Danh mục")]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Giam gia ")]
        public int DiscountId { get; set; }
        [Required]
        [DisplayName("Hình ảnh")]
        public IFormFile Image { get; set; }
        [Required]

        [DisplayName("Gía")]

        public decimal Price { get; set; }
        [Required]

        [DisplayName("Số lượng")]
        public int Quantity { get; set; }
        [Required]

        [DisplayName("Mô tả")]
        public string Description { get; set; }
    }
}
