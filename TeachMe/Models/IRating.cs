using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models
{
    public interface IRating
    {
        double Mark { get; set; }

        string RaterId { get; set; }
    }
}
