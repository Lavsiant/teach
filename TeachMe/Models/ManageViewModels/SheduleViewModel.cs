using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models.CourseModels;

namespace TeachMe.Models.ManageViewModels
{
    public class SheduleViewModel
    {
        public List<UserCourse> StudentLessons { get; set; }

        public List<UserCourse> TeacherLesson { get; set; }

        public string UserId { get; set; }
    }
}
