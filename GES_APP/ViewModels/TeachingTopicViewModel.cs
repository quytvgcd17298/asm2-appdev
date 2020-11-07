using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GES_APP.Models;

namespace GES_APP.ViewModels
{
    public class TeachingTopicViewModel
    {
        public TeachingTopic TeachingTopic { get; set; }
        public IEnumerable<ApplicationUser> Lecturers { get; set; }
        public IEnumerable<Topic> Topics { get; set; }

    }
}