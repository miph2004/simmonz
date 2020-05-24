using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AppUser AppUser { get; set; }
    }
}
