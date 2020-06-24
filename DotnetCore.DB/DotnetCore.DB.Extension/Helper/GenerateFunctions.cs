using DotNetCore.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using VS.DotNetCore.DB.Extension.Helper;

namespace DotNetCore.DB.Helper
{
    public static class GenerateFunctions
    {
        public static void Methods(string ConnectionString, string ContextBasePath, string ContextFilePath, string NamespaceOfContextFile, string SPName)
        {
            SPInformation storedProcedure = new SPInformation(ConnectionString);

            var newFunction = string.Empty;
            newFunction += Environment.NewLine + "\t\t" + "public virtual ";

            #region Return Type

            int spResult = CreateFunctionResultModel(ContextBasePath, storedProcedure, NamespaceOfContextFile, SPName);
            if (spResult == 0)
            {
                newFunction += "void";
            }
            else if (spResult == 1)
            {
                newFunction += "int?";
            }
            else if (spResult == 2)
            {
                newFunction += "List<" + SPName + "_Result>";
            }
            #endregion

            newFunction += " " + SPName + "(";

            var paramList = storedProcedure.GetSPParameterList(SPName);

            #region Binding Method's Paramter

            bool IsFirstParamCreated = false;
            foreach (var param in paramList)
            {
                var sysDataType = DataTypeHelper.GetSysTypeFromSqlType(param.Type, param.IsNullable);

                if (IsFirstParamCreated)
                    newFunction += ", ";
                newFunction += sysDataType + " " + param.Parameter_name;

                IsFirstParamCreated = true;

            }
            newFunction += ")";
            newFunction += Environment.NewLine + "\t\t" + "{";

            #endregion

            #region Bind Parameter

            foreach (var param in paramList)
            {
                newFunction += NLTab(3) + "var " + param.Parameter_name + "Parameter = ";

                var sysDataType = DataTypeHelper.GetSysTypeFromSqlType(param.Type, param.IsNullable);
                if (param.IsNullable && sysDataType != "string")
                {
                    newFunction += param.Parameter_name + ".HasValue ?";
                }
                else
                {
                    newFunction += param.Parameter_name + " != null ?";
                }
                newFunction +=
                    NLTab(4) + "new SqlParameter(\"" + param.Parameter_name.ToString() + "\", " + param.Parameter_name + ") :" +
                    NLTab(4) + "new SqlParameter(\"" + param.Parameter_name.ToString() + "\", typeof(" + sysDataType + "));";

                newFunction += Environment.NewLine;
            }
            #endregion

            #region Bind Function Calling 

            if (spResult == 0)
            {
                newFunction += NLTab(3) + "executeFunction.ExecuteNonQuery(\"" + SPName.ToString() + "\"";
            }
            else if (spResult == 1)
            {
                newFunction += NLTab(3) + "return executeFunction.ExecuteScalar(\"" + SPName.ToString() + "\"";
            }
            else if (spResult == 2)
            {
                newFunction += NLTab(3) + "return executeFunction.ExecuteReader<" + SPName + "_Result" + ">(\"" + SPName.ToString() + "\"";
            }

            foreach (var param in paramList)
            {
                newFunction += ", " + param.Parameter_name + "Parameter";
            }
            newFunction += ");";

            #endregion

            newFunction += Environment.NewLine + "\t\t" + "}";
            newFunction = newFunction + Environment.NewLine + "\t\t" + "//####WriteHere####";

            var allText = File.ReadAllText(ContextFilePath);
            allText = allText.Replace("//####WriteHere####", newFunction);

            File.WriteAllText(ContextFilePath, allText);
        }

        static int CreateFunctionResultModel(string DirBasePath, SPInformation storedProcedure, string NamespaceOfContextFile, string SPName)
        {
            var resultSetList = storedProcedure.GetSPResultSet(SPName).OrderBy(x => x.column_ordinal).ToList();

            //Check what return by SP
            if (resultSetList.Count == 1)
            {
                return 1;
            }
            else if (resultSetList.Count > 1)
            {
                #region Create Class File Text

                var ModelsPath = Path.Combine(DirBasePath, "Models");
                if (!Directory.Exists(ModelsPath))
                {
                    Directory.CreateDirectory(ModelsPath);
                }

                var newModelPath = Path.Combine(ModelsPath, SPName + "_Result.cs");

                string tempClass = string.Empty;

                //TODO
                tempClass += "namespace " + NamespaceOfContextFile + ".Models";
                tempClass += Environment.NewLine + "{";

                tempClass += Environment.NewLine + "\t" + "using System;";
                tempClass += Environment.NewLine + "\t" + "public partial class " + SPName.ToString() + "_Result";

                tempClass += Environment.NewLine + "\t" + "{";

                foreach (var item in resultSetList)
                {
                    tempClass += Environment.NewLine + "\t\t" + "public";
                    var sysDataType = DataTypeHelper.GetSysTypeFromSqlType(item.system_type_name, item.is_nullable);

                    tempClass += " " + sysDataType + " " + item.name.Replace("-", "_");

                    tempClass += " { get; set; }";
                }

                tempClass += Environment.NewLine + "\t" + "}";

                tempClass += Environment.NewLine + "}";

                File.WriteAllText(newModelPath, tempClass);

                #endregion

                return 2;
            }
            else
            {
                return 0;
            }
        }

        static string NLTab(int no = 0)
        {
            string temp = Environment.NewLine;
            for (int i = 0; i < no; i++)
            {
                temp += "\t";
            }
            return temp;
        }
    }
}
