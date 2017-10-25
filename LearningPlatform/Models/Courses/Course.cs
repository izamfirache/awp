using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Courses
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }

        public bool Featured { get; set; }
    }
}