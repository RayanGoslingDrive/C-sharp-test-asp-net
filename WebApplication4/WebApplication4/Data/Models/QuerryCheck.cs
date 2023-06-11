using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data.Models
{
    public class QuerryCheck
    {
        public int TotalFiles { get; set; }
        public int TotalCorrectFiles { get; set; }
        public int TotalIncorrectFiles { get; set; }
        public string[]? IncorrectFilenames { get; set; }

    }

    
}
