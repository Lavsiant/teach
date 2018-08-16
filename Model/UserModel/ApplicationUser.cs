using Microsoft.AspNetCore.Identity;
using Model.CourseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.UserModel
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Marks = new List<TeacherRating>();
            LessonsList = new List<UserCourse>();
            Certificats = new List<CString>();
            StudentCourses = new List<CString>();
            CreatedCourses = new List<Course>();
            Stream = new StreamInfo();
            SummaryStudentsNumber = 0;
        }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public String City { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }

        // public List<Course> LeadingCourses { get; set; }

        public List<UserCourse> LessonsList { get; set; }

        public List<TeacherRating> Marks { get; set; }

        [StringLength(300)]
        public string Biography { get; set; }

        public bool IsTeacher { get; set; }

        public double FinalRating { get; set; }

        [Required]
        [Display(Name = "Appear.In ")]
        public string Skype { get; set; }

        public int SummaryStudentsNumber { get; set; }

        public string ImageName { get; set; }

        public StreamInfo Stream { get; set; }

        public List<CString> Certificats { get; set; }

        public List<CString> StudentCourses { get; set; }

        public List<Course> CreatedCourses { get; set; }

        public List<Comment> Comments { get; set; }
    }
}