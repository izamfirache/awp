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
		private DataRepository<Tag> _tagRepository;
		private DataRepository<CourseTag> _courseTagRepository;
		private DataRepository<CourseRating> _courseRatingsRepository;

		public CoursesController()
		{
			var connectionString = Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine);

			_repository = new DataRepository<Course>(connectionString);
			_tagRepository = new DataRepository<Tag>(connectionString);
			_courseTagRepository = new DataRepository<CourseTag>(connectionString);
			_courseRatingsRepository = new DataRepository<CourseRating>(connectionString);
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
		[Route("Courses/latest")]
		public IHttpActionResult GetLatestCourse()
		{
			var queryBuilder = new SqlQueryBuilder();
			var queryExecutor = new SqlQueryExecutor();

			queryBuilder.AddFreeSql("select Top (1) * from courses order by CreationDate desc");

			var dataTable = queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());

			var course = new Course((DataRow)dataTable.Rows[0]);

			return Json(course);
		}

		[HttpGet]
		[Route("Courses/featured")]
		public IHttpActionResult GetFeaturedCourses()
		{
			return Json(_repository.GetByProperty("IsFeatured", 1));
		}

		[HttpGet]
		[Route("Courses/{courseId}/tags")]
		public IHttpActionResult GetTagsForCourse(int courseId)
		{
			var courseTags = _courseTagRepository.GetByProperty("CourseId", courseId);
			var tags = new List<Tag>();

			foreach (var courseTag in courseTags)
			{
				tags.Add(_tagRepository.GetById(courseTag.TagId));
			}

			return Json(tags);
		}

		[HttpPost]
		public IHttpActionResult Post([FromBody]Course course)
		{
			course.CreationDate = DateTime.Now;
			course.UpdateDate = DateTime.Now;
			var result = _repository.Insert(course);

			if (result != 1)
			{
				return BadRequest("Could not add course");
			}

			if (course.Tags.Count > 0)
			{
				foreach (var tag in course.Tags)
				{
					var existingTag = _tagRepository.GetByProperty("Name", tag.Name).ToList().FirstOrDefault();

					if(existingTag == null)
					{
						_tagRepository.Insert(new Tag()
						{
							Name = tag.Name
						});

						existingTag = _tagRepository.GetByProperty("Name", tag.Name).ToList().FirstOrDefault();
					}

					var addedCourse = _repository.GetByProperty("Name", course.Name).ToList().FirstOrDefault();

					_courseTagRepository.Insert(new CourseTag()
					{
						CourseId = addedCourse.Id,
						TagId = existingTag.Id
					});
				}
			}

			return Ok();
		}

		[HttpGet]
		[Route("Courses/highestRated")]
		public IHttpActionResult GetHighestRatedCourse()
		{
			var query = @"
									select top (5) 
										c.Id
										, c.Name
										, c.Description
										, c.CreationDate
										, c.UpdateDate
										, c.IsFeatured
										, round(AVG(Cast(cr.Rating as Float)), 2) as Rating 
									from dbo.Courses c 
									inner join dbo.CoursesRatings cr on cr.CourseId = c.Id 
									group by 
										c.Id
										, c.Name
										, c.Description
										, c.CreationDate
										, c.UpdateDate
										, c.IsFeatured 
									order by Rating desc";

			var queryBuilder = new SqlQueryBuilder();
			var queryExecutor = new SqlQueryExecutor();

			queryBuilder.AddFreeSql(query);

			var dataTable = queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());

			var coursesWithRating = new List<Course>();

			foreach (DataRow row in dataTable.Rows)
			{
				var course = new Course(row);
				course.Rating = Double.Parse(row["Rating"].ToString());

				coursesWithRating.Add(course);
			}

			return Json(coursesWithRating);
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
