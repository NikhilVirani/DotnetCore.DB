using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DotnetCore.DB
{
    public class ExecuteQuery
    {
        public static string _connectionString;
        public ExecuteQuery(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        public void CallExecuteNonQuery(string query)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int? CallExecuteScalar(string query)
        {
            int? tempId = null;
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                var firstColumn = command.ExecuteScalar();

                if (firstColumn != null)
                {
                    tempId = Convert.ToInt32(firstColumn);
                }
                connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return tempId;
        }
        public List<T> CallExecuteReader<T>(string query)
        {
            List<T> result = new List<T>();
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                var dataReader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                result = DataTableHelper.ConvertDataTable<T>(dataTable);
                connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
    }
}
