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

		public Course()
		{

		}

		public Course(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			Name = row["Name"].ToString();
			Description = row["Description"].ToString();
			CreationDate = DateTime.Parse(row["CreationDate"].ToString());
			UpdateDate = DateTime.Parse(row["UpdateDate"].ToString());
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Courses";
		}
	}
}
