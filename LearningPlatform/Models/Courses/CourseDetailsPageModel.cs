using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Courses
{
    public class CourseDetailsPageModel
    {
        public UserEnrollmentTypeEnum UserCourseStatus { get; set; }
        public Course CurrentCourse { get; set; }
    }
}