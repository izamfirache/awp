﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public class Responsibility : DatabaseEntity
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int CourseId { get; set; }
		public int ResponsibilityTypeId { get; set; }

		public Responsibility()
		{

		}

		public Responsibility(DataRow row)
		{
			Id = Int32.Parse(row["Id"].ToString());
			UserId = Int32.Parse(row["UserId"].ToString());
			CourseId = Int32.Parse(row["CourseId"].ToString());
			ResponsibilityTypeId = Int32.Parse(row["ResponsibilityTypeId"].ToString());
		}

		public override string GetDatabaseTableName()
		{
			return "dbo.Responsibilities";
		}

		public override string GetInsertStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"({UserId}, {CourseId}, {ResponsibilityTypeId})");

			return stringBuilder.ToString();
		}

		public override string GetUpdateStatement()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.Append($" UserId = {UserId}, CourseId = {CourseId}, ResponsibilityTypeId =  {ResponsibilityTypeId} ");

			return stringBuilder.ToString();
		}

		public override string GetInsertFields()
		{
			return "(UserId, CourseId, ResponsibilityTypeId)";
		}
	}
}