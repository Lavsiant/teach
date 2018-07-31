using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TeachMe.Models.CourseModels;

namespace TeachMe.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            //LeadingCourses = new List<Course>();
            Marks = new List<TeacherRating>();
            LessonsList = new List<UserCourse>();
            Certificats = new List<CString>();
            StudentCourses = new List<CString>();
            CreatedCourses = new List<CString>();
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

        public List<CString> CreatedCourses { get; set; }

        public List<Comment> Comments { get; set; }
        // public List<Course> ActiveCourse { get; set; }


    }
}
