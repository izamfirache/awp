using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Courses
{
	public class CourseBuilderPageModel
	{
		public Course CurrentCourse { get; set; }
		public HttpPostedFileBase Thumbnail { get; set; }
	}
}