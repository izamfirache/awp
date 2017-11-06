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
	public class UsersController : ApiController
	{
		private DataRepository<User> _repository;

		public UsersController()
		{
			_repository = new DataRepository<User>(Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine));
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
		public IHttpActionResult Post([FromBody]User user)
		{
			var checkExistingUser = _repository.GetByProperty("Username", user.Username);

			if(checkExistingUser?.Count() > 0)
			{
				return BadRequest("Username already used");
			}

			var checkExistingEmail = _repository.GetByProperty("Email", user.Email);

			if(checkExistingEmail?.Count() > 0)
			{
				return BadRequest("Email already used");
			}

			var result = _repository.Insert(user);

			if(result == 1)
			{
				return Ok();
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

			if(existingUser != null)
			{
				var result = _repository.Update(id, value);

				if(result == 1)
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

			if(existingUser != null)
			{
				var result = _repository.Delete(id);

				if(result == 1)
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
