using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UserModel
{
    public interface IRating
    {
        double Mark { get; set; }

        string RaterId { get; set; }
    }
}
