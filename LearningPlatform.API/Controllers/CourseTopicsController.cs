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
    public class CourseTopicsController : ApiController
    {
        private DataRepository<CourseTopic> _courseTopicsRepository;

        public CourseTopicsController()
        {
            var connectionString = Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine);

            _courseTopicsRepository = new DataRepository<CourseTopic>(connectionString);
        }

        // GET: api/CourseTopics
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(_courseTopicsRepository.GetAll());
        }

        // GET: api/CourseTopics/5
        [HttpGet]
        [Route("courseTopics/{courseTopicId}")]
        public IHttpActionResult Get(int courseTopicId)
        {
            return Json(_courseTopicsRepository.GetById(courseTopicId));
        }

        // POST: api/CourseTopics
        [HttpPost]
        public IHttpActionResult Post([FromBody]CourseTopic value)
        {
            _courseTopicsRepository.Insert(value);
            return Ok();
        }

        // PUT: api/CourseTopics/5
        [HttpPut]        
        public IHttpActionResult Put(int id, [FromBody]CourseTopic value)
        {
            _courseTopicsRepository.Update(id, value);
            return Ok();
        }

        // DELETE: api/CourseTopics/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _courseTopicsRepository.Delete(id);
            return Ok();
        }        
    }
}
