using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Users
{
	public class EditProfileUserModel
	{
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		public string ConfirmNewPassword { get; set; }

		[DataType(DataType.Upload)]
		public byte[] Avatar { get; set; }
	}
}