using LearningPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL
{
	// Generic class for retrieving database entities defined in LearningPlatform.Core that extend "DatabaseEntity" class
	// TODO: configure connection string from a environment variable
	public class DataRepository<T> where T : DatabaseEntity
	{
		// Connection string example: "Server=ISS3\\SQLEXPRESS;Initial Catalog=LearningPlatform.SQL;Integrated Security=true;MultipleActiveResultSets=False;Connection Timeout=30;"

		private string _databaseTableName;
		private SqlQueryExecutor _queryExecutor;

		public DataRepository(string connectionString)
		{            
			SqlQueryExecutor.CONNECTION_STRING = connectionString;

			_queryExecutor = new SqlQueryExecutor();
			var instance = (DatabaseEntity)Activator.CreateInstance(typeof(T));
			_databaseTableName = instance.GetDatabaseTableName();
		}

		// Get by primary key ID
		public T GetById(int id)
		{
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			queryBuilder.AddWhere($"Id = {id}");

			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());
			return GetListFromDataTableRows(results).FirstOrDefault();
		}

		// Get by property
		// e.g. coursesRepo.GetByProperty("Name", "Radical American Symbols Since 1881");
		public IEnumerable<T> GetByProperty(string propertyName, string value)
		{
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			queryBuilder.AddWhere($"{propertyName} = '{value}'");

			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());
			return GetListFromDataTableRows(results);
		}

		// Get by integer property
		// e.g. coursesRepo.GetByProperty("UserId", "2");
		public IEnumerable<T> GetByProperty(string propertyName, int value)
		{
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			queryBuilder.AddWhere($"{propertyName} = {value}");

			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());
			return GetListFromDataTableRows(results);
		}

		// Get all rows of a table
		public IEnumerable<T> GetAll()
		{			
			var queryBuilder = new SqlQueryBuilder();

			queryBuilder.AddSelect(_databaseTableName);
			var results = _queryExecutor.ExecuteSqlReturnDataTable(queryBuilder.GetQuery());

			return GetListFromDataTableRows(results);
		}				

		// create a new row with values from the value parameter
		public int Insert(T value)
		{
			var queryBuilder = new SqlQueryBuilder();
			queryBuilder.AddInsert(_databaseTableName, value.GetInsertStatement());

			return _queryExecutor.ExecuteSql(queryBuilder.GetQuery());
		}

		// id = primary key id
		// value = object with all fields defined
		// DOES NOT SUPPORT PARTIAL UPDATES
		public int Update(int id, T value)
		{
			var queryBuilder = new SqlQueryBuilder();
			queryBuilder.AddUpdate(_databaseTableName, id, value.GetUpdateStatement());

			return _queryExecutor.ExecuteSql(queryBuilder.GetQuery());
		}

		// id = primary key id
		public int Delete(int id)
		{
			var queryBuilder = new SqlQueryBuilder();
			queryBuilder.AddDelete(_databaseTableName, id);

			return _queryExecutor.ExecuteSql(queryBuilder.GetQuery());
		}

		private IEnumerable<T> GetListFromDataTableRows(DataTable dataTable)
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
