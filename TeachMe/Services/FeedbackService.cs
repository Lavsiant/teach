using Microsoft.EntityFrameworkCore;
using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models;
using TeachMe.Models.CourseModels;

namespace TeachMe.Services
{
    public class FeedbackService
    {
        public bool CheckIfCourseRaterValid(string userId, ApplicationUser user, Course course)
        {

            if (course.TeacherID == userId)
            {
                return false;
            }

            bool raterMarker = false;

            foreach (var item in user.LessonsList)
            {
                if (item.CourseTittle == course.Title)
                {
                    raterMarker = true;
                }
            }
            if (raterMarker)
            {
                foreach (var mark in course.Marks)
                {
                    if (mark.RaterId == userId)
                    {
                        raterMarker = false;
                    }
                }
            }
            return raterMarker;
        }

        public bool CheckIfProfileRaterValid(ApplicationUser rater, ApplicationUser teacher)
        {
            bool raterMarker = false;
            if (rater.Id == teacher.Id)
            {
                return false;
            }

            foreach (var item in rater.LessonsList)
            {
                if (item.TeacherId == teacher.Id)
                {
                    raterMarker = true;
                }
            }

            if (raterMarker)
            {
                foreach (var mark in teacher.Marks)
                {
                    if (mark.RaterId == rater.Id)
                    {
                        raterMarker = false;
                    }
                }
            }
            return raterMarker;
        }

        public bool CheckIfValidToSubscribe(string courseTitle, ApplicationUser user)
        {
            bool marker = true;

            foreach (var item in user.LessonsList)
            {
                if (item.CourseTittle.Equals(courseTitle))
                {
                    marker = false;
                }
            }
            return marker;
        }

        public bool CheckIfCommentatorValid(ApplicationUser commentator,ApplicationUser teacher)
        {
            bool result = false;
            foreach (var item in commentator.StudentCourses)
            {
                foreach (var c in teacher.CreatedCourses)
                {
                    if (item.value.Equals(c.Title))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool CheckIfReadyForCreate(ApplicationUser teacher)
        {
            //var courses = context.Course.Where(x => x.TeacherID == teacher.Id && x.IsActive).ToList();
            //if (courses.Count > 0)
            //{
            //    return false;
            //}
            //else
            //{
                return true;
           // }
          
        }

        public Course RateCourse(string mark, string raterId, Course course)
        {

            int m = Convert.ToInt32(mark);
            course.Marks.Add(new CourseMark() { Mark = m, RaterId = raterId });
            course.FinalRating = course.Marks.Average(x => x.Mark);
            return course;
        }

        public ApplicationUser RateTeacher(int mark,string raterId,ApplicationUser teacher)
        {
            teacher.Marks.Add(new TeacherRating() { Mark = mark, RaterId = raterId });
            teacher.FinalRating = teacher.Marks.Select(x => x.Mark).Average();
            return teacher;
        }
    }
}
