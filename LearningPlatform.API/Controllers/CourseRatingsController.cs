using LearningPlatform.Core.Entities;
using LearningPlatform.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearningPlatform.API.Controllers
{
    [Route("courses/courseratings")]
    public class CourseRatingsController : ApiController
    {
        private DataRepository<CourseRating> _repository;
        private DataRepository<Course> _courseRepository;

        public CourseRatingsController()
        {
            _repository = new DataRepository<CourseRating>(Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine));
        }

        [HttpGet]
        public IHttpActionResult GetCourseRatings(int courseId)
        {
            return Json(_repository.GetByProperty("CourseId", courseId));
        }

        [HttpGet]
        public IHttpActionResult GetCourseRatingOfUser(int courseId, [FromUri]int userId)
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("CourseId", courseId);
            dictionary.Add("UserId", userId);

            return Json(_repository.GetByProprieties(dictionary).FirstOrDefault());
        }

        [HttpPost]
        public IHttpActionResult CreateCourseRating([FromBody] CourseRating courseRating)
        {
            var dictionary = new Dictionary<string, int>();
            dictionary.Add("CourseId", courseRating.CourseId);
            dictionary.Add("UserId", courseRating.UserId);

            var exitingCourseRating = _repository.GetByProprieties(dictionary).FirstOrDefault();

            if (exitingCourseRating != null)
            {
                return BadRequest($"User with ID: {courseRating.UserId} already rated course with ID {courseRating.CourseId}");
            }

            courseRating.RatingDate = DateTime.Now;
            var result = _repository.Insert(courseRating);

            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Could not insert new rating");
            }
        }
    }
}
