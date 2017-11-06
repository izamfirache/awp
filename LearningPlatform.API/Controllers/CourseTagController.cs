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
	public class CourseTagController : ApiController
	{
		private DataRepository<CourseTag> _repository;

		public CourseTagController()
		{
			_repository = new DataRepository<CourseTag>(Environment.GetEnvironmentVariable("AWP_DB"));
		}

		// GET: api/Tags
		[HttpGet]
		public IHttpActionResult Get()
		{
			return Json(_repository.GetAll());
		}

		// GET: api/Tags/5
		[HttpGet]
		public IHttpActionResult Get(int id)
		{
			return Json(_repository.GetById(id));
		}

		// POST: api/Tags
		[HttpPost]
		public IHttpActionResult Post([FromBody]CourseTag courseTag)
		{
			var result = _repository.Insert(courseTag);

			if (result == 1)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT: api/Tags/5
		[HttpPut]
		public IHttpActionResult Put(int id, [FromBody]CourseTag value)
		{
			var existingCourse = _repository.GetById(id);

			if (existingCourse != null)
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

		// DELETE: api/CourseTag/5
		[HttpDelete]
		public IHttpActionResult Delete(int id)
		{
			var existingCourse = _repository.GetById(id);

			if (existingCourse != null)
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
