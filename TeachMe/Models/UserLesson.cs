using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models.CourseModels;

namespace TeachMe.Models
{
    public class UserLesson
    {

        public String CourseTitle { get; set; }

        public DateTime StartTime { get; set; }       

        public DateTime EndTime { get; set; }

        public UserShortInfo TeacherInfo { get; set; }

        public UserShortInfo StudentInfo { get; set; }


    }
}
