using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCore.Extension.Helper
{
    public static class DataTypeHelper
    {
        public static string GetSysTypeFromSqlType(string DBType, bool IsNullable)
        {
            DBType = DBType.ToLower().Trim();

            if (DBType == "int")
                return "int" + (IsNullable ? "?" : "");
            else if (DBType == "smallint")
                return "int" + (IsNullable ? "?" : "");
            else if (DBType == "tinyint")
                return "Byte" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("decimal") > -1)
                return "decimal" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("nvarchar") > -1)
                return "string";
            else if (DBType.IndexOf("varchar") > -1)
                return "string";
            else if (DBType.IndexOf("char") > -1)
                return "string";
            else if (DBType.IndexOf("smalldatetime") > -1)
                return "DateTime" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("datetimeoffset") > -1)
                return "DateTimeOffset" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("datetime") > -1)
                return "DateTime" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("date") > -1)
                return "DateTime" + (IsNullable ? "?" : "");
            else if (DBType == "decimal")
                return "decimal" + (IsNullable ? "?" : "");
            else if (DBType == "bit")
                return "bool" + (IsNullable ? "?" : "");
            else if (DBType == "uniqueidentifier")
                return "Guid" + (IsNullable ? "?" : "");
            else if (DBType == "float")
                return "float" + (IsNullable ? "?" : "");
            else if (DBType == "real")
                return "Single" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("time") > -1)
                return "TimeSpan" + (IsNullable ? "?" : "");
            else if (DBType.IndexOf("numeric") > -1)
                return "Decimal" + (IsNullable ? "?" : "");
            else
                throw new Exception("DbType of this '" + DBType + "' is unknown for me");
        }
    }
}
