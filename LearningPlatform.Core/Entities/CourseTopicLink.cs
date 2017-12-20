using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
    public class CourseTopicLink : DatabaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int CourseTopicId { get; set; }

        public CourseTopicLink()
        {

        }

        public CourseTopicLink(DataRow row)
        {
            Id = Int32.Parse(row["Id"].ToString());
            Name = row["Name"].ToString();
            Link = row["Link"].ToString();
            CourseTopicId = Int32.Parse(row["CourseTopicId"].ToString());
        }

        public override string GetDatabaseTableName()
        {
            return "dbo.CourseTopicsLinks";
        }

        public override string GetInsertFields()
        {
            return "(Name, Link, CourseTopicId)";
        }

        public override string GetInsertStatement()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"('{Name}', '{Link}', '{CourseTopicId}')");

            return stringBuilder.ToString();
        }

        public override string GetUpdateStatement()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($" Name = '{Name}', Link = '{Link}', CourseTopicId = '{CourseTopicId}'");

            return stringBuilder.ToString();
        }
    }
}
