using LearningPlatform.Core.Entities;
using LearningPlatform.Helpers;
using LearningPlatform.Models.Homepage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var model = new IndexPageModel();

			var httpClient = new HttpClient();
			var latestCourseRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/courses/latest");
			var result = httpClient.SendAsync(latestCourseRequest);

			var allCoursesRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/courses/featured");
			var allCoursesResult = httpClient.SendAsync(allCoursesRequest);

			var highestRatedCourseRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + "api/courses/highestrated");
			var highestRatedCoursesResult = httpClient.SendAsync(highestRatedCourseRequest);

			Task.WaitAll(new Task[] { result, allCoursesResult, highestRatedCoursesResult });

			model.LatestCourse = JsonConvert.DeserializeObject<Course>(result.Result.Content.ReadAsStringAsync().Result);
			model.FeaturedCourses = JsonConvert.DeserializeObject<List<Course>>(allCoursesResult.Result.Content.ReadAsStringAsync().Result);
			model.HighestRatedCourses = JsonConvert.DeserializeObject<List<Course>>(highestRatedCoursesResult.Result.Content.ReadAsStringAsync().Result);

			var latestCourseTagsRequest = new HttpRequestMessage(HttpMethod.Get, Request.GetBaseUrl() + $"api/courses/{model.LatestCourse.Id}/tags");
			var latestCourseTagResult = httpClient.SendAsync(latestCourseTagsRequest);
			model.LatestCourse.Tags = JsonConvert.DeserializeObject<List<Tag>>(latestCourseTagResult.Result.Content.ReadAsStringAsync().Result);

			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}