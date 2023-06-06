using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data.Models
{
    public class MainThing
    {
        public scanInfo scan { get; set; }

        public Files[] files { get; set; }
    }
}
