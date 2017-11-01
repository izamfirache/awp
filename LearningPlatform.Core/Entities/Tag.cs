using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class Tag : DatabaseEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Tag()
		{

		}

		public Tag(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			Name = row["Name"].ToString();
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Tags";
		}

		public override string GetInsertFields()
		{
			return "(Name)";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" ('{Name}') ");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" Name = '{Name}' ");

			return stringBuilder.ToString();
		}
	}
}
