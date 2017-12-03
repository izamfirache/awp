using LearningPlatform.Core.Entities;
using LearningPlatform.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.Models.Courses
{
    public class CoursesListPageModel
    {
        public List<Course> Courses { get; set; }
        public List<SelectListItem> SortOptions { get; set; }

        public string SelectedSortOptions { get; set; }

        public string Filter { get; set; }
    }
}