using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL
{
	public class SqlQueryBuilder
	{
		private string _query;
		private bool _isWhereAdded;

		public SqlQueryBuilder()
		{

		}

		public SqlQueryBuilder AddSelect(string table)
		{
			_query = _query + $"SELECT * FROM {table}\n";
			return this;
		}

		public SqlQueryBuilder AddWhere(string whereClause)
		{
			if (_isWhereAdded)
			{
				_query = _query + $"AND {whereClause}";
			}
			else
			{
				_query = _query + $"WHERE {whereClause}";
			}
			return this;
		}

		public string GetQuery()
		{
			return _query;
		}
	}
}
