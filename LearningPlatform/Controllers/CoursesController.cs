using LearningPlatform.Models;
using LearningPlatform.Models.Courses;
using LearningPlatform.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.Controllers
{
    public class CoursesController : Controller
    {
        private List<Course> Courses = new List<Course>();
        private List<SelectListItem> SortOptions = new List<SelectListItem>();
        private CoursesListPageModel model = new CoursesListPageModel();
        public CoursesController()
        {
            var coursesProvider = new CourseProvider();
            Courses = coursesProvider.GetCourses();
            SortOptions = new List<SelectListItem>()
            {
                new SelectListItem(){Selected = true, Value = "Sort by title", Text="Sort by title"},
                new SelectListItem(){Selected = false, Value = "Sort by Publish Date", Text="Sort by Publish Date"}
            };
            model.Courses = Courses;
            model.SortOptions = SortOptions;
        }
        public ActionResult GetCourses()
        {
            return View("CourseList", model);
        }

        public ActionResult GetCourse(int courseId)
        {
            var course = Courses.Where(c => c.Id == courseId).FirstOrDefault();
            return View("CourseDetails", course);
        }

        public ActionResult GetFilteredCourses(string filter)
        {
            var newModel = new CoursesListPageModel
            {
                SortOptions = model.SortOptions,
                SelectedSortOptions = model.SelectedSortOptions,
                Filter = model.Filter
            };

            if(filter == "title")
            {
                newModel.Courses = model.Courses.Where(c => c.Title.Contains(filter)).ToList();
            }else if(filter == "date")
            {
                newModel.Courses = model.Courses.OrderByDescending(c => c.PublishDate).ToList();
            }
            else
            {
                return View("CourseList", model);
            }

            return View("CourseList", newModel);
        }
    }
}