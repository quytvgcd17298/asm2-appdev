using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GES_APP.Models;

namespace GES_APP.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public string StudentID { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Student { get; set; }
        public Course Course { get; set; }
    }
}