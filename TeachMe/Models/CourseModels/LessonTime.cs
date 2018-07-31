using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels
{
    public class LessonTime
    {
        public int ID { get; set; }

        [Range(0,4)]
        public int Hours { get; set; }

        [Range(0,59)]
        public int Minutes { get; set; }
    }
}
