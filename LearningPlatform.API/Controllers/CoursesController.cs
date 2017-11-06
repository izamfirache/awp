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
	public class CoursesController : ApiController
	{
		private DataRepository<Course> _repository;

		public CoursesController()
		{
			_repository = new DataRepository<Course>(Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine));
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

		// POST: api/Users
		[HttpPost]
		public IHttpActionResult Post([FromBody]Course course)
		{
			var result = _repository.Insert(course);

			if (result == 1)
			{
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT: api/Users/5
		[HttpPut]
		public IHttpActionResult Put(int id, [FromBody]Course value)
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

		// DELETE: api/Users/5
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
