using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data.Interfaces;
using WebApplication4.Data.Models;

namespace WebApplication4.Data.mocks
{
    public class mockinfo : _Interface
    {
        public IEnumerable<scanInfo> All { get; set; }
        
    }
}
