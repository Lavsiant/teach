using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UserModel
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
