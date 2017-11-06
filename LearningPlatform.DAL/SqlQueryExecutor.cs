using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DAL
{
	public class SqlQueryExecutor
	{
		public static string CONNECTION_STRING { get; set; }

		public SqlQueryExecutor()
		{

		}

		public int ExecuteSql(string sqlQuery)
		{
			using (var connection = new SqlConnection(CONNECTION_STRING))
			{
				connection.Open();
				var command = new SqlCommand(sqlQuery, connection);
				var changedRows = command.ExecuteNonQuery();
				return changedRows;
			}
		}

		public DataTable ExecuteSqlReturnDataTable(string sqlQuery)
		{
			using (var connection = new SqlConnection(CONNECTION_STRING))
			{
				connection.Open();
				var command = new SqlCommand(sqlQuery, connection);
				var results = command.ExecuteReader();
				var dataTable = new DataTable();
				dataTable.Load(results);
				return dataTable;
			}
		}

        public DataTable ExecuteStoredProcReturnDataTable(string storedProcedureName, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                foreach(var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }                

                var results = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(results);
                return dataTable;
            }
        }
	}
}
