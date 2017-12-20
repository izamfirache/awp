using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class Course : DatabaseEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime UpdateDate { get; set; }
		public int IsFeatured { get; set; }
		public double Rating { get; set; }
		public List<Tag> Tags { get; set; }
		public List<CourseTopic> CourseTopics { get; set; }
		public string Thumbnail { get; set; }
		public string ContentHtml { get; set; }

		public Course()
		{
			Tags = new List<Tag>();
			CourseTopics = new List<CourseTopic>();
		}

		public Course(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			Name = row["Name"].ToString();
			Description = row["Description"].ToString();
			CreationDate = DateTime.Parse(row["CreationDate"].ToString());
			UpdateDate = DateTime.Parse(row["UpdateDate"].ToString());
			IsFeatured = Int32.Parse(row["IsFeatured"].ToString());
			ContentHtml = row["ContentHtml"].ToString();

			Tags = new List<Tag>();
			CourseTopics = new List<CourseTopic>();
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Courses";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"('{Name}', '{Description}', '{CreationDate.ToString()}', '{UpdateDate}', {IsFeatured}, '{ContentHtml}')");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" Name = '{Name}', Description = '{Description}', UpdateDate = '{UpdateDate}', IsFeatured = {IsFeatured}, ContentHtml = '{ContentHtml}' ");

			return stringBuilder.ToString();
		}

		public override string GetInsertFields()
		{
			return "(Name, Description, CreationDate, UpdateDate, IsFeatured, ContentHtml)";
		}
	}
}
