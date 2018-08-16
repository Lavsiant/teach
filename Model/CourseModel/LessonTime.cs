using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.CourseModel
{
    public class LessonTime
    {
        public int ID { get; set; }

        [Range(0, 4)]
        public int Hours { get; set; }

        [Range(0, 59)]
        public int Minutes { get; set; }
    }
}
