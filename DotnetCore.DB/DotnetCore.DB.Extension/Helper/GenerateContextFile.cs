using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetCore.DB.Helper
{
    public static class GenerateContextFile
    {
        public static void Create(string ContextBasePath, string ContextFilePath, string NamespaceOfContextFile, string ContextClassName)
        {
            if (!string.IsNullOrWhiteSpace(ContextFilePath)
                && !string.IsNullOrWhiteSpace(NamespaceOfContextFile)
                && !string.IsNullOrWhiteSpace(ContextClassName))
            {
                string temp = string.Empty;

                temp += "using Microsoft.EntityFrameworkCore;";
                temp += nLTab() + "using System;";
                temp += nLTab() + "using System.Collections.Generic;";
                temp += nLTab() + "using DotNetCore.DB;";
                temp += nLTab() + "using System.Data.SqlClient;";
                if (!string.IsNullOrWhiteSpace(NamespaceOfContextFile))
                {
                    temp += nLTab() + "using " + NamespaceOfContextFile + ".Models;";
                }
                temp += nLTab(0);
                temp += nLTab() + "namespace " + NamespaceOfContextFile;
                temp += nLTab() + "{";

                temp += nLTab(1) + "public partial class " + ContextClassName + " : DbContext";
                temp += nLTab(1) + "{";
                temp += nLTab(2) + "private readonly ExecuteFunction executeFunction;";
                temp += nLTab(0);
                temp += nLTab(2) + "public " + ContextClassName + "(string connectionString)";
                temp += nLTab(2) + "{";
                temp += nLTab(3) + "executeFunction = new ExecuteFunction(connectionString);";
                temp += nLTab(2) + "}";
                temp += nLTab(2) + "//####WriteHere####";
                temp += nLTab(1) + "}";
                temp += nLTab() + "}";

                if (File.Exists(ContextFilePath))
                {
                    File.WriteAllText(ContextFilePath, temp);
                }
                else
                {
                    if (!Directory.Exists(ContextBasePath))
                    {
                        Directory.CreateDirectory(ContextBasePath);
                    }

                    File.WriteAllText(ContextFilePath, temp);
                }
            }
            else
            {
                //TODO thrown error which are null
            }
        }

        static string nLTab(int no = 0)
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
