using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearningPlatform.Models.Users
{
    public class EditProfileUserModel
    {        
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmNewPassword { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public HttpPostedFileBase NewAvatar { get; set; }
    }
}