using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Users
{
	public class LoggedInUserModel
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public byte[] Avatar { get; set; }
	}
}