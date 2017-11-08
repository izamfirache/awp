using LearningPlatform.Core.Entities;
using LearningPlatform.DAL;
using System;
using System.Collections.Generic;
using System.Data;
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
		public IHttpActionResult GetAllCourses()
		{
			return Json(_repository.GetAll());
		}

		// GET: api/Users/5
		[HttpGet]
		public IHttpActionResult GetCourseById(int id)
		{
			return Json(_repository.GetById(id));
		}

		[HttpGet]
		public IHttpActionResult GetCourseByName(string courseName)
		{
			return Json(_repository.GetByProperty("Name", courseName));
		}

		[HttpGet]
		public IHttpActionResult GetCoursesByOwnerId([FromUri]string ownerId)
		{
			// call DAL directly because it doesnt not return a single entity
			var queryExecutor = new SqlQueryExecutor();

			var parameters = new Dictionary<string, object>();
			parameters.Add("ownerId", ownerId);

			var dataTable = queryExecutor.ExecuteStoredProcReturnDataTable("dbo.GetCoursesByOwnerId", parameters);

			var resultList = new List<Course>();

			foreach (var row in dataTable.Rows)
			{
				resultList.Add(new Course((DataRow)row));
			}

			return Json(resultList);
		}

		[HttpGet]
		public IHttpActionResult GetCoursesByTag([FromUri]string tagName)
		{
			// call DAL directly because it doesnt not return a single entity
			var queryExecutor = new SqlQueryExecutor();

			var parameters = new Dictionary<string, object>();
			parameters.Add("tagName", tagName);

			var dataTable = queryExecutor.ExecuteStoredProcReturnDataTable("dbo.GetCoursesByTag", parameters);

			var resultList = new List<Course>();

			foreach (var row in dataTable.Rows)
			{
				resultList.Add(new Course((DataRow)row));
			}

			return Json(resultList);
		}

		[HttpGet]
		[Route("courses/latest")]
		public IHttpActionResult GetLatestCourse()
		{
			var queryBuilder = new SqlQueryBuilder();
			var queryExecutor = new SqlQueryExecutor();

			queryBuilder.AddFreeSql("select Top (1) * from courses order by CreationDate desc");

			var dataTable = queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());

			var course = new Course((DataRow)dataTable.Rows[0]);

			return Json(course);
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
