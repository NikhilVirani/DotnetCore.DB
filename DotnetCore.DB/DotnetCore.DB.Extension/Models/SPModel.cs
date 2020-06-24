using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.DB.Models
{
    public class SPModel
    {
        public string name { get; set; }
        public int id { get; set; }
        public DateTime crdate { get; set; }
        public DateTime refdate { get; set; }
    }
}
