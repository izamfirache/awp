using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Courses
{
	public class RegisterCourse
	{
		[Required]
		public string CourseName { get; set; }

		[Required]
		public string Description { get; set; }

		public HttpPostedFileBase Thumbnail { get; set; }
	}
}