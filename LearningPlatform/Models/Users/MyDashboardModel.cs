using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Users
{
    public class MyDashboardModel
    {
        public List<Course> ActiveCourses;
        public List<Course> PendingCourses;
        public List<Course> CompletedCourses;

        public MyDashboardModel()
        {
            ActiveCourses = new List<Course>();
            PendingCourses = new List<Course>();
            CompletedCourses = new List<Course>();
        }
    }
}