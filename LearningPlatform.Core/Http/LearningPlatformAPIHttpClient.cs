using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Http
{
	public class LearningPlatformAPIHttpClient : HttpClient
	{
		public const string COURSES_ENDPOINT = "api/courses/";
		public const string COURSE_TAG_ENDPOINT = "api/coursetag/";
		public const string RESPONSIBILITY_ENDPOINT = "api/responsibility/";
		public const string RESPONSIBILITY_TYPES_ENDPOINT = "api/responsibilitytypes/";
		public const string TAG_ENDPOINT = "api/tags/";
		public const string USERS_ENDPOINT = "api/users/";
	}
}
