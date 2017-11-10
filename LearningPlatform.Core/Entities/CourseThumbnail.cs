using LearningPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class CourseThumbnail : DatabaseEntity
	{
		public int Id { get; set; }
		public int CourseId { get; set; }
		public string Thumbnail { get; set; }

		public CourseThumbnail()
		{

		}

		public CourseThumbnail(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			CourseId = Int32.Parse(row["CourseId"].ToString());

			if (!(row["Thumbnail"] is DBNull))
			{
				Thumbnail = ((byte[])row["Thumbnail"]).ByteArrayToString();
			}
			else
			{
				Thumbnail = string.Empty;
			}
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.CoursesThumbnails";
		}

		public override string GetInsertFields()
		{
			return "(CourseId, Thumbnail)";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" ({CourseId}, 0x{Thumbnail}) ");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" Username = '{CourseId}', Thumbnail = 0x{Thumbnail} ");

			return stringBuilder.ToString();
		}
	}
}
