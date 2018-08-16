using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.CourseModel
{
    public class UserCourse
    {
        public int ID { get; set; }     

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
