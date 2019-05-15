using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels.CourseViewModels
{
    public class SubscribeViewModel
    {
        [HiddenInput]
        public Course Course { get; set; }

        public List<DayOfWeek> WorkDays { get; set; }

        public int LessonNumber { get; set; }
        public IList<CourseLesson> Lessons { get; set; }
    }
}
