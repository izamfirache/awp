using LearningPlatform.Core.Entities;
using LearningPlatform.DAL;
using LearningPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LearningPlatform.API.Controllers
{
		public class CoursesController : ApiController
		{
				private DataRepository<Course> _repository;
				private DataRepository<Tag> _tagRepository;
				private DataRepository<CourseTag> _courseTagRepository;
				private DataRepository<CourseRating> _courseRatingsRepository;
				private DataRepository<CourseThumbnail> _courseThumbnailsRepository;
				private DataRepository<CourseTopic> _courseTopicRepository;
				private DataRepository<CourseTopicLink> _courseTopicLinksRepository;

				public CoursesController()
				{
						var connectionString = Environment.GetEnvironmentVariable("AWP_DB", EnvironmentVariableTarget.Machine);

						_repository = new DataRepository<Course>(connectionString);
						_tagRepository = new DataRepository<Tag>(connectionString);
						_courseTagRepository = new DataRepository<CourseTag>(connectionString);
						_courseTopicRepository = new DataRepository<CourseTopic>(connectionString);
						_courseRatingsRepository = new DataRepository<CourseRating>(connectionString);
						_courseTopicLinksRepository = new DataRepository<CourseTopicLink>(connectionString);
						_courseThumbnailsRepository = new DataRepository<CourseThumbnail>(connectionString);
				}

				// GET: api/Users
				[HttpGet]
				public IHttpActionResult GetAllCourses()
				{
						return Json(_repository.GetAll());
				}

				[HttpGet]
				[Route("courses/{courseId}")]
				public IHttpActionResult GetCourseById(int courseId)
				{
						return Json(_repository.GetById(courseId));
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

				[HttpGet]
				[Route("Courses/{courseId}/topics")]
				public IHttpActionResult GetTopicsForCourse(int courseId)
				{
						var courseTopics = _courseTopicRepository.GetByProperty("CourseId", courseId);

						foreach(var courseTopic in courseTopics)
						{
								courseTopic.CourseTopicLinks = _courseTopicLinksRepository.GetByProperty("CourseTopicId", courseTopic.Id).ToList();
						}

						return Json(_courseTopicRepository.GetByProperty("CourseId", courseId));
				}

				[HttpPost]
				[Route("Courses/{courseId}/topics")]
				public IHttpActionResult GetTopicsForCourse(int courseId, [FromBody]List<CourseTopic> value)
				{
						value.ForEach(v => _courseTopicRepository.Insert(v));
						return Ok();
				}

				[HttpGet]
				[Route("Courses/{courseId}/thumbnail")]
				public HttpResponseMessage GetCourseAvatar(int courseId)
				{
						var course = _repository.GetById(courseId);

						if (course == null)
						{
								return new HttpResponseMessage(HttpStatusCode.NotFound);
						}

						var thumbnail = _courseThumbnailsRepository.GetByProperty("CourseId", courseId)?.FirstOrDefault()?.Thumbnail;

						if(thumbnail != null)
						{
								var imageStream = thumbnail.StringToByteArray().ToStream();
								var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(imageStream) };
								response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
								return response;
						}
						else
						{
								return new HttpResponseMessage(HttpStatusCode.NotFound);
						}						
				}

				[HttpPost]
				public IHttpActionResult Post([FromBody]Course course)
				{
						course.CreationDate = DateTime.Now;
						course.UpdateDate = DateTime.Now;
						var result = _repository.Insert(course);
						var addedCourse = _repository.GetByProperty("Name", course.Name).ToList().FirstOrDefault();

						if (result != 1)
						{
								return BadRequest("Could not add course");
						}

						if (course.Tags != null && course.Tags.Count > 0)
						{
								foreach (var tag in course.Tags)
								{
										var existingTag = _tagRepository.GetByProperty("Name", tag.Name).ToList().FirstOrDefault();

										if (existingTag == null)
										{
												_tagRepository.Insert(new Tag()
												{
														Name = tag.Name
												});

												existingTag = _tagRepository.GetByProperty("Name", tag.Name).ToList().FirstOrDefault();
										}

										_courseTagRepository.Insert(new CourseTag()
										{
												CourseId = addedCourse.Id,
												TagId = existingTag.Id
										});
								}
						}

						if (course.Thumbnail != null)
						{
								_courseThumbnailsRepository.Insert(new CourseThumbnail()
								{
										CourseId = addedCourse.Id,
										Thumbnail = course.Thumbnail
								});
						}

						return Ok();
				}

				[HttpGet]
				[Route("Courses/highestRated")]
				public IHttpActionResult GetHighestRatedCourse()
				{
						var query = @"
									select top (4) 
										c.Id
										, c.Name
										, c.Description
										, c.CreationDate
										, c.UpdateDate
										, c.IsFeatured
										, c.ContentHtml
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
										, c.ContentHtml
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
				// TODO: update tags of the updated course
				public IHttpActionResult Put(int id, [FromBody]Course value)
				{
						var existingCourse = _repository.GetById(id);

						if (existingCourse != null)
						{
								value.UpdateDate = DateTime.Now;
								var result = _repository.Update(id, value);

								if (result != 1)
								{
										return BadRequest();
								}

								// insert tags that are not defined in the database
								foreach (var tag in value.Tags)
								{
										if (_tagRepository.GetByProperty("Name", tag.Name).Count() == 0)
										{
												_tagRepository.Insert(new Tag()
												{
														Name = tag.Name
												});
										}
								}

								// check for just deleted tags
								//foreach(var course)

								// check for new tags
						}
						else
						{
								return NotFound();
						}

						return Ok();
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
