using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.DB.Models
{
    public class SPResultModel
    {
        public int column_ordinal { get; set; }
        public bool is_nullable { get; set; }
        public string system_type_name { get; set; }
        public string name { get; set; }
    }
}
