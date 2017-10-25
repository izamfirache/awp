using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class ResponsibilityType : DatabaseEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }		

		public ResponsibilityType()
		{

		}

		public ResponsibilityType(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			Name = row["Name"].ToString();
			Description = row["Description"].ToString();
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.ResponsibilityTypes";
		}
	}
}
