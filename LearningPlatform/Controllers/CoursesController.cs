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

            // get the tags
            var url = Request.GetBaseUrl() + "api/tags";
            var getTagsRequest =  new HttpRequestMessage(HttpMethod.Get, url);
            var getTagsRequestResult = httpClient.SendAsync(getTagsRequest);
            model.Tags = JsonConvert.DeserializeObject<List<Tag>>
                (getTagsRequestResult.Result.Content.ReadAsStringAsync().Result);

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

            var enrollmentRequest = new HttpRequestMessage(
                HttpMethod.Get,
                Request.GetBaseUrl() + string.Format("api/userenrollment?userId={0}&courseId={1}", Session["LoggedInUserId"], courseId)
                );

            var enrollmentResult = httpClient.SendAsync(enrollmentRequest).Result;

            if (enrollmentResult.IsSuccessStatusCode)
            {
                var enrollment = JsonConvert.DeserializeObject<UserEnrollment>(enrollmentResult.Content.ReadAsStringAsync().Result);
                courseDetailsPageModel.UserCourseStatus = enrollment.UserEnrollmentType;
            }           

            return View("CourseDetails", courseDetailsPageModel);
        }

        public ActionResult GetFilteredCoursesByDate()
        {
            var newModel = new CoursesListPageModel();
            var courses = GetLatestCourses();
            newModel.Courses = courses.OrderByDescending(c => c.CreationDate).ToList();
            newModel.Tags = GetTags();

            return View("CourseList", newModel);
        }

        public ActionResult SearchCourses(string filter)
        {
            var newModel = new CoursesListPageModel();
            var courses = GetLatestCourses();
            newModel.Courses = courses.Where(c => c.Name.ToLower().Contains(filter)).ToList();
            newModel.Tags = GetTags();

            return View("CourseList", newModel);
        }

        public List<Course> GetLatestCourses()
        {
            var httpClient = new HttpClient();
            var currentCourseRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/courses");
            var result = httpClient.SendAsync(currentCourseRequest);
            var courses = JsonConvert.DeserializeObject<List<Course>>
                (result.Result.Content.ReadAsStringAsync().Result);

            return courses;
        }

        public ActionResult SearchCoursesByTags(string tag)
        {
            var newModel = new CoursesListPageModel();

            var httpClient = new HttpClient();
            var url = Request.GetBaseUrl() + string.Format("api/courses?tagName={0}", tag);
            var getCoursesByTagsRequest = new HttpRequestMessage(HttpMethod.Get, url);
            var getCoursesByTagsRequestResult = httpClient.SendAsync(getCoursesByTagsRequest);

            newModel.Courses = JsonConvert.DeserializeObject<List<Course>>
                (getCoursesByTagsRequestResult.Result.Content.ReadAsStringAsync().Result);
            newModel.Tags = GetTags();
            newModel.SelectedTagId = newModel.Tags.Where(t => t.Name == tag).FirstOrDefault().Id;

            return View("CourseList", newModel);
        }

        private List<Tag> GetTags()
        {
            // get the tags
            var httpClient = new HttpClient();
            var tagsurl = Request.GetBaseUrl() + "api/tags";
            var getTagsRequest = new HttpRequestMessage(HttpMethod.Get, tagsurl);
            var getTagsRequestResult = httpClient.SendAsync(getTagsRequest);
            return JsonConvert.DeserializeObject<List<Tag>>
                (getTagsRequestResult.Result.Content.ReadAsStringAsync().Result);
        }
    }
}