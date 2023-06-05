using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data.Models;

namespace WebApplication4.Data.Interfaces
{
    public interface _Files
    {
        IEnumerable<Files> All { get;}
    }
}
