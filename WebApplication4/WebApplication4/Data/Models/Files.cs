using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data.Models
{
    public class Files
    {
        public string filename { get; set; }
        public bool result { get; set; }

        public errorss[] errors { get; set; }

        public DateTimeOffset? scantime { get; set; }
    }

}
