using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GES_APP.Models;

namespace GES_APP.ViewModels
{
    public class ManagerStaffViewModel
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string WorkingPlace { get; set; }
        public int Phone { get; set; }
        public List<ManagerStaffViewModel> Student { get; set; }
        public List<ManagerStaffViewModel> Lecturer { get; set; }
        public object[] Id { get; internal set; }
    }
}