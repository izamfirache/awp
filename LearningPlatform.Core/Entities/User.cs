using LearningPlatform.Helpers;
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
		public string Avatar { get; set; }

		public User()
		{
		}

		public User(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public User(string username, string password, string email) : this(username, password)
		{
			Email = email;
		}

		public User(string username, string password, string email, string avatar) : this(username, password, email)
		{
			Avatar = avatar;
		}

		public User(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			Username = row["Username"].ToString();
			Password = row["Password"].ToString();
			Email = row["Email"].ToString();

			if (!(row["Avatar"] is DBNull))
			{
				Avatar = ((byte[])row["Avatar"]).ByteArrayToString();
			}
			else
			{
				Avatar = string.Empty;
			}
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Users";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"('{Username}', '{Password}', '{Email}', 0x{Avatar})");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" Username = '{Username}', Password = '{Password}', Email = '{Email}', Avatar = 0x{Avatar} ");

			return stringBuilder.ToString();
		}

		public override string GetInsertFields()
		{
			return "(Username, Password, Email)";
		}
	}
}
