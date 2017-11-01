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
	public class ResponsibilityTypesController : ApiController
	{
		private DataRepository<ResponsibilityType> _repository;

		public ResponsibilityTypesController()
		{
			_repository = new DataRepository<ResponsibilityType>();
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
		public IHttpActionResult Post([FromBody]ResponsibilityType responsibilityType)
		{
			var result = _repository.Insert(responsibilityType);

			if (result == 1)
			{
				return Ok();
			}
			else
			{
				return BadRequest("Error when inserting new responsibility type");
			}
		}

		// PUT: api/Users/5
		[HttpPut]
		public IHttpActionResult Put(int id, [FromBody]ResponsibilityType responsibilityType)
		{
			var exitingResponsibilityType = _repository.GetById(id);

			if (exitingResponsibilityType != null)
			{
				var result = _repository.Update(id, responsibilityType);

				if (result == 1)
				{
					return Ok();
				}
				else
				{
					return BadRequest("Error when updating responsibility type");
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
			var exitingResponsibilityType = _repository.GetById(id);

			if (exitingResponsibilityType != null)
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
