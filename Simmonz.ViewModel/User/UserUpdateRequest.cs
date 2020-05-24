using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Simmonz.ViewModel.User
{
   public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DayOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
