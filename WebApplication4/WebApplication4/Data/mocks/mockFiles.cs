using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data.Interfaces;
using WebApplication4.Data.Models;

namespace WebApplication4.Data.mocks
{
    public class mockFiles : _Files
    {
        public IEnumerable<Files> All { get; set; }
    }
}
