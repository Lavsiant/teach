using Model.CourseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels.CourseViewModels
{
    public class CreateCourseViewModel
    {
        public CreateCourseViewModel()
        {
           WeekDays = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => new CDayOfWeak(x)).ToList();
        }

        public Course Course { get; set; }

        public List<CDayOfWeak> WeekDays { get; set; }
    }
}
