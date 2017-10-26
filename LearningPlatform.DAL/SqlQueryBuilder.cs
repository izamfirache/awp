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

		/// <summary>
		/// Adds Select from table
		/// </summary>
		/// <param name="table">FROM table</param>
		/// <returns>SqlQueryBuilder for decorating a sql query</returns>
		public SqlQueryBuilder AddSelect(string table)
		{
			_query = _query + $"SELECT * FROM {table}\n";
			return this;
		}

		/// <summary>
		/// Adds Where condition. Handles multiple conditions
		/// </summary>
		/// <param name="whereClause"> WHERE/ADD whereClause </param>
		/// <returns>SqlQueryBuilder for decorating a sql query</returns>
		public SqlQueryBuilder AddWhere(string whereClause)
		{
			if (_isWhereAdded)
			{
				_query = _query + $" AND {whereClause} ";
			}
			else
			{
				_query = _query + $" WHERE {whereClause} ";
				_isWhereAdded = true;
			}
			return this;
		}

		/// <summary>
		/// Adds Insert to query
		/// </summary>
		/// <param name="table">INSERT INTO {table}</param>
		/// <param name="values">VALUES {values}</param>
		/// <returns>SqlQueryBuilder for decorating a sql query</returns>
		public SqlQueryBuilder AddInsert(string table, string values)
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append($"INSERT INTO {table} values {values}");

			_query = stringBuilder.ToString();

			return this;
		}

		/// <summary>
		/// Adds Update to query to row with Id = id. DOES NOT SUPPORT PARTIAL UPDATES
		/// </summary>
		/// <param name="table">UPDATE {table}</param>
		/// <param name="id">WHERE {id}</param>
		/// <param name="set">SET {set}</param>
		/// <returns></returns>
		public SqlQueryBuilder AddUpdate(string table, int id, string set)
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append($"UPDATE {table} SET {set} where Id = ({id})");

			_query = stringBuilder.ToString();

			return this;
		}

		/// <summary>
		/// Adds Delete to query. Deletes row with Id = id
		/// </summary>
		/// <param name="table">DELETE FROM {table}</param>
		/// <param name="id">WHERE ID = {id}</param>
		/// <returns></returns>
		public SqlQueryBuilder AddDelete(string table, int id)
		{
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.Append($"DELETE FROM {table} WHERE ID = {id}");

			_query = stringBuilder.ToString();

			return this;
		}

		public string GetQuery()
		{
			return _query;
		}
	}
}
