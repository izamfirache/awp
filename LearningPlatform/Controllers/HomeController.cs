using LearningPlatform.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var coursesCollection = new CourseCollection();
            return View(coursesCollection);
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