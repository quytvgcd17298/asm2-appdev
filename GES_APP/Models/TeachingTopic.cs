using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GES_APP.Models
{
    public class TeachingTopic
    {
        public int Id { get; set; }
        public string LecturerID { get; set; }
        public int TopicId { get; set; }

        public ApplicationUser Lecturer { get; set; }
        public Topic Topic { get; set; }

    }
}