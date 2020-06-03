using Microsoft.AspNetCore.Http;
using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Simmonz.ViewModel.Product
{
   public class ProductUpdateRequest
    {
        [Required]
        [DisplayName("Mã")]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }
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
        [Required]
        [DisplayName("Danh mục")]
        public int CategoryId { get; set; }
        public int DiscountId { get; set; }

    }
}
