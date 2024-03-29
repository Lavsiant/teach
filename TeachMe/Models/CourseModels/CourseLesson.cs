﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels
{
    public class CourseLesson
    {
        [Key]
        public int ID { get; set; }

        public DayOfWeek WeekDay { get; set; }

        public DateTime StartLessonTime { get; set; }      

        public DateTime EndLessonTime { get; set; }

        public bool isBusy { get; set; }

        public DateTime BusyExpireDate { get; set; }
    }
}
