using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class CourseRating : DatabaseEntity
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public int UserId { get; set; }
		public int Rating { get; set; }
		public DateTime RatingDate { get; set; }

		public CourseRating()
		{

		}

		public CourseRating(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			CourseId = Int32.Parse(row["CourseId"].ToString());
			UserId = Int32.Parse(row["UserId"].ToString());
			Rating = Int32.Parse(row["Rating"].ToString());
			RatingDate = DateTime.Parse(row["RatingDate"].ToString());
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.CoursesRatings";
		}

		public override string GetInsertFields()
		{
			throw new NotImplementedException();
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"({CourseId}, {UserId}, {Rating}, '{RatingDate}')");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			throw new NotImplementedException();
		}
	}
}
