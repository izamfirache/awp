using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
    public class UserEnrollment : DatabaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEnrollmentType UserEnrollmentTypeId { get; set; }
        public int CourseId { get; set; }

        public UserEnrollment()
        {

        }

        public UserEnrollment(DataRow row)
        {
            Id = Int32.Parse(row["Id"].ToString());
            UserId = Int32.Parse(row["UserId"].ToString());
            UserEnrollmentTypeId = (UserEnrollmentType)Int32.Parse(row["UserEnrollmentTypeId"].ToString());
            CourseId = Int32.Parse(row["CourseId"].ToString());
        }

        public override string GetDatabaseTableName()
        {
            return "dbo.UserEnrollments";
        }

        public override string GetInsertFields()
        {
            return "(UserId, UserEnrollmentTypeId, CourseId)";
        }

        public override string GetInsertStatement()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"('{UserId}', '{UserEnrollmentTypeId}', '{CourseId}')");

            return stringBuilder.ToString();
        }

        public override string GetUpdateStatement()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($" UserId = '{UserId}', UserEnrollmentTypeId = '{UserEnrollmentTypeId}', CourseId = '{CourseId}' ");

            return stringBuilder.ToString();
        }
    }

    public enum UserEnrollmentType
    {
        All,
        Pending,
        Active,
        Completed
    }
}
