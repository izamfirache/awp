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
	public class ResponsibilityController : ApiController
	{
		private DataRepository<Responsibility> _repository;

		public ResponsibilityController()
		{
			_repository = new DataRepository<Responsibility>();
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
		public IHttpActionResult Post([FromBody]Responsibility responsibility)
		{
			var user = new DataRepository<User>().GetById(responsibility.UserId);

			if(user == null)
			{
				return BadRequest($"Could not find user with ID {responsibility.UserId}");
			}

			var course = new DataRepository<Course>().GetById(responsibility.CourseId);

			if(course == null)
			{
				return BadRequest($"Could not find course with ID {responsibility.CourseId}");
			}

			var responsibilityType = new DataRepository<ResponsibilityType>().GetById(responsibility.ResponsibilityTypeId);

			if(responsibilityType == null)
			{
				return BadRequest($"Could not find responsibility type with ID {responsibility.ResponsibilityTypeId}");
			}

			var result = _repository.Insert(responsibility);

			if (result == 1)
			{
				return Ok();
			}
			else
			{
				return BadRequest("Error when adding the new responsibility");
			}
		}

		// PUT: api/Users/5
		[HttpPut]
		public IHttpActionResult Put(int id, [FromBody]Responsibility responsibility)
		{
			var existingResponsibility = _repository.GetById(id);

			if (existingResponsibility != null)
			{
				var result = _repository.Update(id, responsibility);

				if (result == 1)
				{
					return Ok();
				}
				else
				{
					return BadRequest("Error when updating responsibility");
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
					return BadRequest("Error when deleting responsibility");
				}
			}
			else
			{
				return NotFound();
			}
		}
	}
}
