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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserEnrollment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserEnrollment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserEnrollment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserEnrollment/5
        public void Delete(int id)
        {
        }
    }
}
