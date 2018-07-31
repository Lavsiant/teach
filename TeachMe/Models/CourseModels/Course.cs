using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Models.CourseModels
{
    public class Course
    {
        public int ID { get; set; }

        public Course()
        {
            WeekPlans = new List<CDayOfWeak>() { };          
            LessonSchedule = new List<CourseLesson>();
            Duration = new LessonTime();
            SummaryStudentsNumber = 0;
        //    var y = typeof(Course).GetProperty("LessonNumber").GetCustomAttributes(true).FirstOrDefault(x => x is RangeAttribute) as RangeAttribute;
          
        }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }    


        [Range(1, 1000)]
        [Display(Name = "Month price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.Text)]
        [StringLength(600,MinimumLength = 50)]
        public string Description { get; set; }

        [Range(1, 7)]
        [Display(Name = "Lessons per week")]
        public byte LessonsNumber { get; set; }

        public CourseCategory Category { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Subject { get; set; }
    
        public CourseTeacherInfo TeacherInfo { get; set; }

        public string TeacherID { get; set; }

        [Display(Name = "Week schedule")]
        public List<CDayOfWeak> WeekPlans { get; set; }

        [Display(Name ="Lesson duration")]
        public LessonTime Duration { get; set; }

        [HiddenInput]
        public List<CourseLesson> LessonSchedule { get; set; }

        public List<CourseMark> Marks { get; set; }

        public double FinalRating { get; set; }

        public int SummaryStudentsNumber { get; set; }

        public bool IsActive { get; set; }

    }
}

