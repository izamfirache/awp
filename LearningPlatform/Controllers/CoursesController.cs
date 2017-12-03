using LearningPlatform.Core.Entities;
using LearningPlatform.Helpers;
using LearningPlatform.Models;
using LearningPlatform.Models.Courses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.Controllers
{
    public class CoursesController : Controller
    {
        public ActionResult GetCourses()
        {
            var model = new CoursesListPageModel();

            var httpClient = new HttpClient();
            var currentCourseRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/courses");
            var result = httpClient.SendAsync(currentCourseRequest);
            model.Courses = JsonConvert.DeserializeObject<List<Course>>
                (result.Result.Content.ReadAsStringAsync().Result);
            return View("CourseList", model);
        }

        public ActionResult GetCourse(int courseId)
        {
            var courseDetailsPageModel = new CourseDetailsPageModel();

            var httpClient = new HttpClient();
            var currentCourseRequest = new HttpRequestMessage(
                HttpMethod.Get, 
                Request.GetBaseUrl() + 
                string.Format("api/courses/{0}", courseId));

            var result = httpClient.SendAsync(currentCourseRequest);
            courseDetailsPageModel.CurrentCourse = JsonConvert.DeserializeObject<Course>
                (result.Result.Content.ReadAsStringAsync().Result);

            return View("CourseDetails", courseDetailsPageModel);
        }

        //public ActionResult GetFilteredCourses(string filter)
        //{
        //    var newModel = new CoursesListPageModel
        //    {
        //        SortOptions = model.SortOptions,
        //        SelectedSortOptions = model.SelectedSortOptions,
        //        Filter = model.Filter
        //    };

        //    if(filter == "title")
        //    {
        //        newModel.Courses = model.Courses.Where(c => c.Title.Contains(filter)).ToList();
        //    }else if(filter == "date")
        //    {
        //        newModel.Courses = model.Courses.OrderByDescending(c => c.PublishDate).ToList();
        //    }
        //    else
        //    {
        //        return View("CourseList", model);
        //    }

        //    return View("CourseList", newModel);
        //}
    }
}