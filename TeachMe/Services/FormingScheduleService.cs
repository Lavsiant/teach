using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.CourseModels;

namespace TeachMe.Services
{
    public class FormingScheduleService
    {
        public List<CourseLesson> FormSchedule(List<CDayOfWeak> avalibleDays, LessonTime duration,DateTime startDate,int weekNum)
        {
            const int minutesPerHour = 60;
            int lessonMins = duration.Hours * minutesPerHour + duration.Minutes;
            var schedule = new List<CourseLesson>();
            DateTime curDate = GetFirstWorkDay(avalibleDays, startDate);
            int[] weekDays = (int[])Enum.GetValues(typeof(DayOfWeek));
            int startIndex = weekDays[(int)curDate.DayOfWeek];
            //  DateTime startDate = curDate.AddDays(startIndex);
           
            for (int i =0; i < weekNum; i++)
            {
                for (int j = startIndex; j < 7;)
                {
                    int index = avalibleDays.FindIndex(x => x.IsWorkDay == true && (int)x.WeekDay == j);
                    if (index >= 0)
                    {
                    int startLessonTime = avalibleDays[index].StartTime * minutesPerHour;
                        int endLessonsTime = 0;
                        if (avalibleDays[index].EndTime == 24)
                        {
                            endLessonsTime = avalibleDays[index].EndTime * minutesPerHour - lessonMins - 1;
                        }
                        else
                        {
                            endLessonsTime = avalibleDays[index].EndTime * minutesPerHour - lessonMins;
                        }
                       
                        
                        for (int k = startLessonTime; k <= endLessonsTime; k+=lessonMins)
                        {
                          
                     

                            schedule.Add(new CourseLesson()
                            {
                                StartLessonTime = new DateTime(1, 1, 1, k / minutesPerHour, k - (k / minutesPerHour * minutesPerHour), 0),
                                EndLessonTime = new DateTime(1, 1, 1, (k + lessonMins) / minutesPerHour, (k + lessonMins) - ((k + lessonMins) / minutesPerHour * minutesPerHour), 0),
                                WeekDay = curDate.DayOfWeek,
                                isBusy = false
                            });

                        }
                    }
                    curDate = curDate.AddDays(1);
                    int temp = j + 1;
                    if (temp == startIndex)
                    {
                        break;
                    }
                    if (temp < 7)
                    {
                        j++;
                    }
                    else
                    {
                        j = 0;
                    }


                }
            }






            
            //for (int i = startIndex; i < 7; i++)
            //{
                
            //    if (avalibleDays[i].IsWorkDay)
            //    {
            //        int startTimeTemp = avalibleDays[i].StartTime * minutesPerHour;
            //        int entTime = avalibleDays[i].EndTime * minutesPerHour;
            //        for (int j = 0; j < (avalibleDays[i].EndTime * minutesPerHour - avalibleDays[i].StartTime * minutesPerHour) / (lessonMins); j++)
            //        {
            //            schedule.Lessons.Add(new CourseLesson()
            //            {
            //                StartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTimeTemp / minutesPerHour, startTimeTemp - startTimeTemp / minutesPerHour, 0),
            //                EndTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, (startTimeTemp + lessonMins) / minutesPerHour, (startTimeTemp + lessonMins) - startTimeTemp / minutesPerHour, 0),
            //                Duration = duration,
            //                IsFree = true
            //            });
            //            startTimeTemp += lessonMins;
            //        }
                    
            //    }
            //    startDate = startDate.AddDays(1);
            //    if (i == startIndex - 1)
            //    {
            //        break;
            //    }
            //}
            
            return schedule;
        }

        public List<CourseLesson> GetAvalibleLessons(ApplicationDbContext context, Course course,string userId)
        {
            
            var user = context.Users.Include(x => x.LessonsList).FirstOrDefault(x => x.Id == userId);
            var lessons = new List<CourseLesson>();
            foreach (var lesson in course.LessonSchedule.Where(x => !x.isBusy).OrderBy(x => x.WeekDay).ThenBy(x => x.StartLessonTime))
            {
                bool marker = true;
                foreach (var userLesson in user.LessonsList)
                {
                    if ((lesson.StartLessonTime >= userLesson.StartLessonTime && lesson.StartLessonTime <= userLesson.EndLessonTime) || (lesson.EndLessonTime >= userLesson.StartLessonTime && lesson.EndLessonTime <= userLesson.EndLessonTime))
                    {
                        marker = false;
                    }
                }
                if (marker)
                {
                    lessons.Add(lesson);
                }
            }
            return lessons;
        }

        public List<CourseLesson> GetAvalibleLessonsForTeacher(ApplicationDbContext context, Course course, string userId)
        {

            var user = context.Users.Include(x => x.LessonsList).FirstOrDefault(x => x.Id == userId);
            var usersCourse = context.Course.FirstOrDefault(x => x.IsActive && x.TeacherID == userId);

            var lessons = new List<CourseLesson>();
            foreach (var lesson in course.LessonSchedule.Where(x => !x.isBusy).OrderBy(x => x.WeekDay).ThenBy(x => x.StartLessonTime))
            {
                bool marker = true;
                foreach (var userLesson in usersCourse.WeekPlans)
                {
                    if (lesson.WeekDay == userLesson.WeekDay && (lesson.StartLessonTime.Hour>userLesson.StartTime && lesson.StartLessonTime.Hour<userLesson.EndTime))
                    {
                        marker = false;
                    }
                }
                if (marker)
                {
                    lessons.Add(lesson);
                }
            }
            return lessons;
        }

        public List<UserCourse> FillUsersStudentSchedule( ApplicationUser user, ApplicationDbContext context)
        {
            var studentLessons = new List<UserCourse>();
            foreach (var lesson in user.LessonsList)
            {
                if (String.IsNullOrEmpty(lesson.StudentId))
                {
                    var teacher = context.Users.FirstOrDefault(x => x.Id == lesson.TeacherId);
                    lesson.UserShortInfo = new UserShortInfo() { FirstName = teacher.FirstName, LastName = teacher.LastName, AppearIn=teacher.Skype };
                    studentLessons.Add(lesson);
                }          
            }
            return studentLessons;
        }

        public List<UserCourse> FillUsersTeacherSchedule( ApplicationUser user, ApplicationDbContext context)
        {
            var teacherLessons = new List<UserCourse>();
            foreach (var lesson in user.LessonsList)
            {
                if (String.IsNullOrEmpty(lesson.TeacherId))
                {
                    var student = context.Users.FirstOrDefault(x => x.Id == lesson.StudentId);
                    lesson.UserShortInfo = new UserShortInfo() { FirstName = student.FirstName, LastName = student.LastName};
                    teacherLessons.Add(lesson);
                }
            }
            return teacherLessons;
        }

        private DateTime GetFirstWorkDay(List<CDayOfWeak> avalibleDays, DateTime startDate)
        {
            var weekDays = (int[])Enum.GetValues(typeof(DayOfWeek));
            int startIndex = weekDays[(int)startDate.DayOfWeek];
            for (int i = startIndex; i < 7;)
            {
                int index = avalibleDays.FindIndex(x => x.IsWorkDay == true && (int)x.WeekDay == i);
                if (index>=0)
                {
                    var date = startDate;
                    return date;
                }

                startDate = startDate.AddDays(1);

                var temp = i + 1;
                if (temp < 7)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }

                        
            }
            return DateTime.Now;
        }

        
    }

   
}
