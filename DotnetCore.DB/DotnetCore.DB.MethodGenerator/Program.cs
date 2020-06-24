using DotnetCore.DB.Extension.Helper;
using System;
using System.IO;
using System.Linq;

namespace DotnetCore.DB.MethodGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"--Your DB Connection String--";
            string ContextBasePath = @"--Directory of where context file creation--";
            string ContextClassName = "--Context Class name--";
            string NamespaceOfContextFile = "--Namespace Of Context Class--";
            string ContextFilePath = Path.Combine(ContextBasePath, ContextClassName + ".cs");

            SPInformation storedProcedure = new SPInformation(ConnectionString);
            var list = storedProcedure.GetSPList().ToList();

            if (list.Count > 0)
            {
                GenerateContextFile.Create(
                    ContextBasePath,
                    ContextFilePath,
                    NamespaceOfContextFile,
                    ContextClassName);

                int i = 0;
                foreach (var item in list)
                {
                    Console.WriteLine(item.name + "- Start");
                    GenerateFunctions.Methods(ConnectionString, ContextBasePath, ContextFilePath, NamespaceOfContextFile, item.name);
                    Console.WriteLine(item.name + "- End");
                    Console.WriteLine("--------------------------------------------------" + i++);
                }
            }
            else
            {
                Console.WriteLine("Your database has not any stored procedure.");
            }
        }
    }
}
