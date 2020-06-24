using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VS.DotNetCore.DB
{
    public class ExecuteFunction
    {
        public static string _connectionString;
        public ExecuteFunction(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        public void ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var parameter in parameters)
                {
                    SqlParameter sqlParameter = new SqlParameter
                    {
                        ParameterName = parameter.ParameterName,
                        Value = parameter.Value,
                        SqlDbType = parameter.SqlDbType
                    };
                    command.Parameters.Add(sqlParameter);
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int? ExecuteScalar(string storedProcedureName, params SqlParameter[] parameters)
        {
            int? tempId = null;
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var parameter in parameters)
                {
                    SqlParameter sqlParameter = new SqlParameter
                    {
                        ParameterName = parameter.ParameterName,
                        Value = parameter.Value,
                        SqlDbType = parameter.SqlDbType
                    };
                    command.Parameters.Add(sqlParameter);
                }

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
        public List<T> ExecuteReader<T>(string storedProcedureName, params SqlParameter[] parameters)
        {
            List<T> result = new List<T>();
            try
            {
                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(storedProcedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };


                foreach (var parameter in parameters)
                {
                    SqlParameter sqlParameter = new SqlParameter
                    {
                        ParameterName = parameter.ParameterName,
                        Value = parameter.Value,
                        SqlDbType = parameter.SqlDbType
                    };
                    command.Parameters.Add(sqlParameter);
                }

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
