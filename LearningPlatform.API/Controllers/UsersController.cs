using LearningPlatform.Core.Entities;
using LearningPlatform.DAL;
using LearningPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LearningPlatform.API.Controllers
{
    public class UsersController : ApiController
    {
        private DataRepository<User> _repository;
        private DataRepository<UserEnrollment> _userEnrollmentRepositry;
        private DataRepository<Course> _coursesRepository;

        public UsersController()
        {
            _repository = new DataRepository<User>(Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine));
            _userEnrollmentRepositry = new DataRepository<UserEnrollment>(Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine));
            _coursesRepository = new DataRepository<Course>(Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine));
        }

        // GET: api/Users
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(_repository.GetAll());
        }

        // GET: api/Users/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Json(_repository.GetById(id));
        }

        [HttpGet]
        [Route("users/{id}/avatar")]
        public HttpResponseMessage GetAvatar(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            var avatar = _repository.GetById(id).Avatar;

            var imageStream = avatar.StringToByteArray().ToStream();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(imageStream) };
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }        

        [HttpGet]
        [Route("users/{userId}/enrollment")]
        public IHttpActionResult GetEnrolledCourses(int userId, [FromUri]string type)
        {
            UserEnrollmentTypeEnum enrollmentType;

            switch (type)
            {
                case "pending":
                    enrollmentType = UserEnrollmentTypeEnum.Pending;
                    break;
                case "active":
                    enrollmentType = UserEnrollmentTypeEnum.Active;
                    break;
                case "completed":
                    enrollmentType = UserEnrollmentTypeEnum.Completed;
                    break;
                default:
                    enrollmentType = UserEnrollmentTypeEnum.All;
                    break;
            }

            var enrollments = _userEnrollmentRepositry.GetByProperty("UserId", userId);

            if (enrollmentType != UserEnrollmentTypeEnum.All)
            {
                enrollments = enrollments.Where(e => e.UserEnrollmentType == enrollmentType);
            }

            var enrolledCourses = new List<Course>();

            foreach (var enrollment in enrollments)
            {
                enrolledCourses.Add(_coursesRepository.GetById(enrollment.CourseId));
            }

            return Json(enrolledCourses);
        }

        // POST: api/Users
        [HttpPost]
        public IHttpActionResult Post([FromBody]User user)
        {
            var checkExistingUser = _repository.GetByProperty("Username", user.Username);

            if (checkExistingUser?.Count() > 0)
            {
                return BadRequest("Username already used");
            }

            var checkExistingEmail = _repository.GetByProperty("Email", user.Email);

            if (checkExistingEmail?.Count() > 0)
            {
                return BadRequest("Email already used");
            }

            var result = _repository.Insert(user);

            if (result == 1)
            {
                var createdUser = _repository.GetByProperty("Username", user.Username).FirstOrDefault();
                return Ok(createdUser);
            }
            else
            {
                return BadRequest("Could not create user");
            }
        }

        [HttpPost]
        [Route("users/token")]
        public IHttpActionResult Token([FromBody]User user)
        {
            var dictionary = new Dictionary<string, string>();

            dictionary.Add("Username", user.Username);
            dictionary.Add("Password", user.Password);

            var results = _repository.GetByProprieties(dictionary);

            if (results.Count() == 1)
            {
                return Json(results.ElementAt(0));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Users/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]User value)
        {
            var existingUser = _repository.GetById(id);

            if (existingUser != null)
            {
                var result = _repository.Update(id, value);

                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingUser = _repository.GetById(id);

            if (existingUser != null)
            {
                var result = _repository.Delete(id);

                if (result == 1)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
