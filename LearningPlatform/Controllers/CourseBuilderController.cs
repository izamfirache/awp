﻿using LearningPlatform.Core.Entities;
using LearningPlatform.Helpers;
using LearningPlatform.HtmlSanitizerHelper;
using LearningPlatform.Models.Courses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.Controllers
{
	public class CourseBuilderController : Controller
	{
		// GET: CourseBuilder
		public ActionResult BuildNewCourse()
		{
			var model = new CourseBuilderPageModel();
			return View("CourseBuilder", model);
		}

		// POST: CourseBuilder
		[ValidateInput(false)]
		[HtmlSanitizer]
		public async Task<ActionResult> AddNewCourse(CourseBuilderPageModel coursebuilderPageModel)
		{
			// TODO: Use GUID's for CourseId
			//coursebuilderPageModel.CurrentCourse.Id = new Random().Next(1000, 10000); 

			string thumbnailData = string.Empty;

			if (coursebuilderPageModel.Thumbnail != null)
			{
				MemoryStream memoryStream = new MemoryStream();
				coursebuilderPageModel.Thumbnail.InputStream.CopyTo(memoryStream);
				thumbnailData = memoryStream.ToArray().ByteArrayToString();
			}

			coursebuilderPageModel.CurrentCourse.Thumbnail = thumbnailData;
            coursebuilderPageModel.CurrentCourse.CourseTopics.Add(coursebuilderPageModel.Topic);

			// save the new course in the database
			var httpClient = new HttpClient();
			var addNewCourseRequest = new HttpRequestMessage(
					HttpMethod.Post,
					Request.GetBaseUrl() + "api/courses");
			var serializedObject = JsonConvert.SerializeObject(coursebuilderPageModel.CurrentCourse);
			addNewCourseRequest.Content = new StringContent(serializedObject, System.Text.Encoding.UTF8, "application/json");

			var model = new CourseBuilderPageModel();
			var result = await httpClient.SendAsync(addNewCourseRequest);

			if (result.IsSuccessStatusCode)
			{
				// remain on the course builder page, with an empty model
				return View("CourseBuilder", model);
			}
			else
			{
				throw new Exception("There was a problem while saving the new course.");
			}
		}

		[ValidateInput(false)] // TODO: IMPLEMENT A CUSTOM SANITIZER ON POST REQUESTS.
		public ActionResult AddUiElementToCourse(CourseBuilderPageModel newUiElementPageModel)
		{
			var model = new CourseBuilderPageModel
			{
				CurrentCourse = new Course()
			};
			model.CurrentCourse.Name = newUiElementPageModel.CurrentCourse.Name;
			model.CurrentCourse.Description = newUiElementPageModel.CurrentCourse.Description;
			model.CurrentCourse.ContentHtml += newUiElementPageModel.CurrentCourse.ContentHtml;

			return View("CourseBuilder", model);
		}
	}
}