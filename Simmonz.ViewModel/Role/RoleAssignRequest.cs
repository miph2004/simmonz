using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.User
{
    public class RoleAssignRequest
    {
        public int Id { get; set; }
        public List<SelectedItem> Roles { get; set; } = new List<SelectedItem>();
    }
}
