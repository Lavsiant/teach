using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels
{
    public class DetailsViewModel
    {
        public Course Course { get; set; }

        public ApplicationUser Teacher { get; set; }

        public bool isReadtForRate { get; set; }

        public bool IsValidForSubscribe  { get; set; }
    }
}
