﻿using LearningPlatform.Core.Entities;
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

        public ActionResult GetFilteredCoursesByDate()
        {
            var newModel = new CoursesListPageModel();
            var courses = GetLatestCourses();
            newModel.Courses = courses.OrderByDescending(c => c.CreationDate).ToList();

            return View("CourseList", newModel);
        }

        public ActionResult SearchCourses(string filter)
        {
            var newModel = new CoursesListPageModel();
            var courses = GetLatestCourses();
            newModel.Courses = courses.Where(c => c.Name.ToLower().Contains(filter)).ToList();

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
    }
}