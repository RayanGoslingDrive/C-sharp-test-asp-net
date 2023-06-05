using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data.Interfaces;
using WebApplication4.Data.Models;

namespace WebApplication4.Data.mocks
{
    public class mockErrors : _Errors
    {
        public IEnumerable<errorss> All { get; set; }

    }
}