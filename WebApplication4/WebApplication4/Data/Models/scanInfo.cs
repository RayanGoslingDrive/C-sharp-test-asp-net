using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data.Models
{
    public class scanInfo
    {
        public DateTimeOffset scanTime { get; set; }
        public String db { get; set; }
        public String server { get; set; }
        public int errorCount;
        public IList<DateTimeOffset> DatesAvailable { get; set; }
        public IList<Files> file { get; set; }

    }

 
}
