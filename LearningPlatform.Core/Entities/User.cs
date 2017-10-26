using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class User : DatabaseEntity
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }		

		public User()
		{			
		}

		public User(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			Username = row["Username"].ToString();
			Password = row["Password"].ToString();
			Email = row["Email"].ToString();
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Users";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"('{Username}', '{Password}', '{Email}')");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" Username = '{Username}', Password = '{Password}', Email = '{Email}' ");

			return stringBuilder.ToString();
		}

		public override string GetInsertFields()
		{
			return "(Username, Password, Email)";
		}
	}
}
