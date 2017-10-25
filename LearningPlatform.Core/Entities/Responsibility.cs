using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class Responsibility : DatabaseEntity
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CourseId { get; set; }
		public int ResponsibilityTypeId { get; set; }

		public Responsibility()
		{

		}

		public Responsibility(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			UserId = Int32.Parse(row["UserId"].ToString());
			CourseId = Int32.Parse(row["CourseId"].ToString());
			ResponsibilityTypeId = Int32.Parse(row["ResponsibilityTypeId"].ToString());
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Responsibilities";
		}
	}
}
