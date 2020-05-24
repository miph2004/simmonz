using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Simmonz.ViewModel.User
{
   public  class RegisterRequest
    {
        [DisplayName("Tên")]
        public string FirstName { get; set; }
        [DisplayName("Họ")]
        public string LastName { get; set; }
        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime DayOfBirth { get; set; }
        [DisplayName("Điện thoại")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Tên tài khoản")]
        public string UserName { get; set; }
        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Xác nhận mật khẩu")]
        public string ConfirmPassWord { get; set; }
    }
}
