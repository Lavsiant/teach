using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels.CourseViewModels
{
    public class HomeViewModel
    {
        public List<Course> NewCourses { get; set; }

        public List<Course> BestCourses { get; set; }

        public List<ApplicationUser> BestTeachers { get; set; }
    }
}
