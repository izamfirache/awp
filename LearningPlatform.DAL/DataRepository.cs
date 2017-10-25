using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL
{
	public class DataRepository<T> where T : DatabaseEntity
	{
		private string _databaseTableName;
		private SqlQueryExecutor _queryExecutor;

		public DataRepository()
		{
			_queryExecutor = new SqlQueryExecutor();
			SqlQueryExecutor.CONNECTION_STRING = "Server=ISS3\\SQLEXPRESS;Initial Catalog=LearningPlatform.SQL;Integrated Security=true;MultipleActiveResultSets=False;Connection Timeout=30;";

			_databaseTableName = ((DatabaseEntity)Activator.CreateInstance(typeof(T))).GetDatabaseTableName();
		}

		public List<T> GetById(int id)
		{
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			queryBuilder.AddWhere($"Id = {id}");

			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());
			return GetListFromDataTableRows(results);
		}

		public List<T> GetByProperty(string propertyName, string value)
		{
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			queryBuilder.AddWhere($"{propertyName} = '{value}'");

			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());
			return GetListFromDataTableRows(results);
		}

		public List<T> GetByProperty(string propertyName, int value)
		{
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			queryBuilder.AddWhere($"{propertyName} = {value}");

			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());
			return GetListFromDataTableRows(results);
		}

		public List<T> GetAll()
		{			
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());

			return GetListFromDataTableRows(results);
		}

		private List<T> GetListFromDataTableRows(DataTable dataTable)
		{
			var resultList = new List<T>();

			foreach (var row in dataTable.Rows)
			{
				resultList.Add((T)Activator.CreateInstance(typeof(T), row));
			}

			return resultList;
		}
	}
}
