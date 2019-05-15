using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels.CourseViewModels
{
    public class IndexCourseViewModel
    {
       
        public List<Course> Courses { get; set; }
        public ApplicationUser User { get; set; }
        public SelectList categories { get; set; }
        public string courseCategory { get; set; }
        public SelectList sortCrtiteriaList { get; set; }
        public string sortCriteria { get; set; }
    }
}
