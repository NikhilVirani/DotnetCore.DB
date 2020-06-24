using DotNetCore.DB;
using DotNetCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore.DB.Extension.Helper
{
    public class SPInformation
    {
        private readonly string _connectionSring;

        public SPInformation(string connectionSring)
        {
            _connectionSring = connectionSring;
        }

        public List<SPModel> GetSPList()
        {
            ExecuteQuery executeQuery = new ExecuteQuery(_connectionSring);
            return executeQuery.CallExecuteReader<SPModel>("SELECT * FROM sysobjects WHERE TYPE = 'P' and category = 0;");
        }

        public List<SPParameterModel> GetSPParameterList(string SPName)
        {
            ExecuteQuery executeQuery = new ExecuteQuery(_connectionSring);
            var query = @"
                SELECT  
	               'Parameter_name' = name,  
	               'Type'   = type_name(user_type_id),  
	               'Length'   = max_length,  
	               'Prec'   = case when type_name(system_type_id) = 'uniqueidentifier' 
				              then precision  
				              else OdbcPrec(system_type_id, max_length, precision) end,  
	               'Scale'   = OdbcScale(system_type_id, scale),  
	               'Param_order'  = parameter_id,  
	               'Collation'   = convert(sysname, 
					               case when system_type_id in (35, 99, 167, 175, 231, 239)  
					               then ServerProperty('collation') end),
					'IsNullable' = is_nullable 
	              from sys.parameters where object_id = object_id(N'" + SPName + @"')";
            return executeQuery.CallExecuteReader<SPParameterModel>(query);
        }

        public List<SPResultModel> GetSPResultSet(string SPName)
        {
            ExecuteQuery executeQuery = new ExecuteQuery(_connectionSring);
            var query = @"
                CREATE TABLE #Result2(
	                is_hidden BIT
	                ,column_ordinal INT
	                ,name NVARCHAR(MAX)
	                ,is_nullable	BIT
	                ,system_type_id	INT
	                ,system_type_name	 NVARCHAR(MAX)
	                ,max_length	INT
	                ,precision	INT
	                ,scale	INT
	                ,collation_name	 NVARCHAR(MAX) NULL
	                ,user_type_id	 NVARCHAR(MAX) NULL
	                ,user_type_database	 NVARCHAR(MAX) NULL
	                ,user_type_schema	 NVARCHAR(MAX) NULL
	                ,user_type_name		 NVARCHAR(MAX) NULL
	                ,assembly_qualified_type_name		 NVARCHAR(MAX) NULL
	                ,xml_collection_id		 NVARCHAR(MAX) NULL
	                ,xml_collection_database	 NVARCHAR(MAX) NULL	
	                ,xml_collection_schema	 NVARCHAR(MAX) NULL
	                ,xml_collection_name		 NVARCHAR(MAX) NULL
	                ,is_xml_document	BIT
	                ,is_case_sensitive		BIT
	                ,is_fixed_length_clr_type		BIT
	                ,source_server	NVARCHAR(MAX) NULL
	                ,source_database	NVARCHAR(MAX) NULL
	                ,source_schema	NVARCHAR(MAX) NULL
	                ,source_table	NVARCHAR(MAX) NULL
	                ,source_column	NVARCHAR(MAX) NULL
	                ,is_identity_column	BIT
	                ,is_part_of_unique_key	BIT NULL
	                ,is_updateable	BIT
	                ,is_computed_column	BIT	
	                ,is_sparse_column_set	BIT	
	                ,ordinal_in_order_by_list		NVARCHAR(MAX) NULL
	                ,order_by_is_descending		NVARCHAR(MAX) NULL
	                ,order_by_list_length		NVARCHAR(MAX) NULL
	                ,tds_type_id	INT 
	                ,tds_length	INT
	                ,tds_collation_id BIGINT	
	                ,tds_collation_sort_id INT
	            )
	            INSERT #Result2 EXEC sp_describe_first_result_set N'" + SPName + @"'
                SELECT * FROM #Result2 ORDER BY Name
	            DROP TABLE #Result2";
            return executeQuery.CallExecuteReader<SPResultModel>(query);
        }
    }
}
