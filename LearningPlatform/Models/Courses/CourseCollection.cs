using LearningPlatform.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Courses
{
    public class CourseCollection
    {
        public CourseCollection()
        {
            var coursesProvider = new CourseProvider();
            FeaturedCourses = coursesProvider.GetFeaturedCourses();
            NewCourses = coursesProvider.GetNewCourses();
        }
        public List<Course> FeaturedCourses { get; set; }
        public List<Course> NewCourses { get; set; }
    }
}