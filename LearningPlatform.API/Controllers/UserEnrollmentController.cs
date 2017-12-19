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
    public class UserEnrollmentController : ApiController
    {
        private DataRepository<UserEnrollment> _userEnrollmentRepository;

        public UserEnrollmentController()
        {
            var connectionString = Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine);

            _userEnrollmentRepository = new DataRepository<UserEnrollment>(connectionString);
        }

        // GET: api/UserEnrollment
        [HttpGet]                
        public IHttpActionResult Get([FromUri]int userId, [FromUri]int courseId)
        {
            var conditions = new Dictionary<string, int>();
            conditions.Add("UserId", userId);
            conditions.Add("CourseId", courseId);

            var result = _userEnrollmentRepository.GetByProprieties(conditions).LastOrDefault();

            if(result == null)
            {
                return NotFound();
            }
            else
            {
                return Json(result);
            }            
        }

        // POST: api/UserEnrollment
        [HttpPost]
        public IHttpActionResult Post([FromBody]UserEnrollment value)
        {
            _userEnrollmentRepository.Insert(value);
            return Ok();
        }

        // DELETE: api/UserEnrollment/5
        [HttpDelete]
        public IHttpActionResult Delete([FromUri]int userId, [FromUri]int courseId)
        {
            var conditions = new Dictionary<string, int>();
            conditions.Add("UserId", userId);
            conditions.Add("CourseId", courseId);

            var userEnrollmentsToDelete = _userEnrollmentRepository.GetByProprieties(conditions);
            userEnrollmentsToDelete.ToList().ForEach(e => _userEnrollmentRepository.Delete(e.Id));
            return Ok();
        }
    }
}
