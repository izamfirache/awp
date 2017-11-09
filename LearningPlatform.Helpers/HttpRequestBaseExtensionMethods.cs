using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LearningPlatform.Helpers
{
	public static class HttpRequestBaseExtensionMethods
	{
		public static string GetBaseUrl(this HttpRequestBase request)
		{
			return request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
		}
	}
}
