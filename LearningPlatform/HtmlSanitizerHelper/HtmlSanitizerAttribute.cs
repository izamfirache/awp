using Ganss.XSS;
using LearningPlatform.Core.Entities;
using LearningPlatform.Models.Courses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.HtmlSanitizerHelper
{
    public class HtmlSanitizerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var htmlSanitizer = new HtmlSanitizer();
            base.OnActionExecuting(filterContext);
            var sanitizedValues = new Dictionary<string, object>();

            // If the request is either a PUT or POST, attempt to decode all strings
            var requestType = filterContext.HttpContext.Request.RequestType.ToString();
            if (requestType == WebRequestMethods.Http.Post)
            {
                // For each of the items in the PUT/POST
                foreach (var item in filterContext.ActionParameters)
                {
                    try
                    {
                        string sanitizedValue = null;
                        string jsonValue = null;
                        object originalObject = null;

                        Type type = item.Value.GetType();
                        if (type == typeof(string))
                        {
                            sanitizedValue = htmlSanitizer.Sanitize(item.Value.ToString());
                        }
                        else if (type == typeof(CourseBuilderPageModel))
                        {
                            var course = (CourseBuilderPageModel)item.Value;
                            if (course.CurrentCourse.ContentHtml.Contains("<script>"))
                            {
                                throw new Exception("Invalid course content.");
                            }
                        }
                        else if (!type.IsPrimitive) // exclude primitive types (besides string type)
                        {
                            jsonValue = JsonConvert.SerializeObject(item.Value);
                            sanitizedValue = htmlSanitizer.Sanitize(jsonValue);
                        }

                        if (!string.IsNullOrEmpty(sanitizedValue))
                        {
                            originalObject = JsonConvert.DeserializeObject(sanitizedValue, type);
                            sanitizedValues.Add(item.Key, originalObject);
                        }
                    }
                    catch (Exception ex)
                    {
                        //if it fails, it's not the end of the world
                        throw new Exception(ex.Message);
                    }
                }

                //filterContext.ActionParameters = sanitizedValues;
            }
        }
    }
}