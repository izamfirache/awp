using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Core.Entities
{
	public abstract class DatabaseEntity
	{
		public abstract string GetDatabaseTableName();
	}
}
