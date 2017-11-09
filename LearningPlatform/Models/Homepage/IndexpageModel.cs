using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Homepage
{
	public class IndexPageModel
	{
		public Course LatestCourse;
		public List<Course> HighestRatedCourses;
		public List<Course> FeaturedCourses;

		public IndexPageModel()
		{
			LatestCourse = new Course();
			HighestRatedCourses = new List<Course>();
			FeaturedCourses = new List<Course>();
		}
	}
}