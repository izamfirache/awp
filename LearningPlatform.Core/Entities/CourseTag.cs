using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class CourseTag : DatabaseEntity
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public int TagId { get; set; }

		public CourseTag()
		{

		}

		public CourseTag(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			CourseId = Int32.Parse(row["CourseId"].ToString());
			TagId = Int32.Parse(row["TagId"].ToString());
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.CoursesTags";
		}

		public override string GetInsertFields()
		{
			return "(CourseId, TagId)";
		}			

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" ({CourseId}, {TagId}) ");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" CourseId = {CourseId}, TagId = {TagId} ");

			return stringBuilder.ToString();
		}
	}
}
