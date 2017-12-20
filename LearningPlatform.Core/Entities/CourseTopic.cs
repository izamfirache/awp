using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class CourseTopic : DatabaseEntity
	{
		public int Id { get; set; }
		public string TopicName { get; set; }
		public string TopicDescription { get; set; }
		public int CourseId { get; set; }
		public List<CourseTopicLink> CourseTopicLinks { get; set; }

		public CourseTopic()
		{
			CourseTopicLinks = new List<CourseTopicLink>();
		}

		public CourseTopic(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			TopicName = row["TopicName"].ToString();
			TopicDescription = row["TopicDescription"].ToString();
			CourseId = Int32.Parse(row["Courseid"].ToString());
			CourseTopicLinks = new List<CourseTopicLink>();
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.CourseTopics";
		}

		public override string GetInsertFields()
		{
			return "(TopicName, TopicDescription, CourseId)";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"('{TopicName}', '{TopicDescription}', '{CourseId}')");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" TopicName = '{TopicName}', TopicDescription = '{TopicDescription}', CourseId = '{CourseId}'");

			return stringBuilder.ToString();
		}
	}
}
