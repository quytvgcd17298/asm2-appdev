using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GES_APP.Models;

namespace GES_APP.ViewModels
{
    public class EnrollmentViewModel
    {
        public Enrollment Enrollment { get; set; }
        public IEnumerable<ApplicationUser> Students { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}