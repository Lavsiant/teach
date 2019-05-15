using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels
{
    public class UserCourse
    {
        public int ID { get; set; }

      //  public ApplicationUser Student { get; set; }

        public string CourseTittle { get; set; }
        
        public DateTime StartLessonTime { get; set; }

        public DateTime EndLessonTime { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public DateTime ExpireDate { get; set; }

        public string TeacherId { get; set; }

        public string StudentId { get; set; }

        public UserShortInfo UserShortInfo { get; set; }
    }
}
