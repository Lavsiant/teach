using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.CourseModel
{
    public class CDayOfWeak
    {

        [Key]
        public int ID { get; set; }

        public CDayOfWeak()
        {
            IsWorkDay = false;
            StartTime = 0;
            EndTime = 24;
        }

        public CDayOfWeak(DayOfWeek dayW) : this()
        {
            WeekDay = dayW;
        }

        [HiddenInput]
        public DayOfWeek WeekDay { get; set; }

        [Range(0, 24)]
        public int StartTime { get; set; }

        [Range(0, 24)]
        public int EndTime { get; set; }

        public bool IsWorkDay { get; set; }

    }
}
