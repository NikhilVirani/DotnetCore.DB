using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.DB.Models
{
    public class SPParameterModel
    {
        public string Parameter_name { get; set; }
        public string Type { get; set; }
        public bool IsNullable { get; set; }
    }
}
