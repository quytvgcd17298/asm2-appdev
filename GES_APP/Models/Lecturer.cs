using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GES_APP.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string WorkingPlace { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Experience { get; set; }
        public string Achievements { get; set; }
        public string Nationality { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

    }
}