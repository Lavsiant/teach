using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.CourseModel
{
    public class CourseTeacherInfo
    {
        [Key]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Skype { get; set; }

        public string Email { get; set; }
    }
}
