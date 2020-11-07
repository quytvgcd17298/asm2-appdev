using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GES_APP.Models
{
    public class Student
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }

    }
}