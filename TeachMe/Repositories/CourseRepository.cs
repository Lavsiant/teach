using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.CourseModels;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Services;

namespace TeachMe.Repositories
{
    public class CourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Course> GetCourseListWithTeacherInfo()
        {
            return _context.Course.Include(x => x.TeacherInfo).Select(x => x).ToList();
        }

        public List<Course> GetCourseListWithLessonSchedule()
        {
            return _context.Course.Include(x => x.LessonSchedule).Select(x => x).ToList();
        }

        public Course GetSingleFullCourse(int? id)
        {
            return _context.Course.Include(x => x.TeacherInfo)
                .Include(x => x.Marks)
                .Include(y => y.WeekPlans)
                .Include(x => x.Duration)
                .Include(x=>x.LessonSchedule)
                .SingleOrDefault(a => a.ID == id);
        }

        public Course GetCourseWithLessonsByTittle(string tittle)
        {
            return _context.Course.Include(x => x.LessonSchedule).FirstOrDefault(x=>x.Title.Equals(tittle));
        }

        public Course GetSingleCourseWithSchedule(int id)
        {
            return _context.Course
                .Include(x => x.LessonSchedule)
                .Include(x=>x.TeacherInfo)
                .FirstOrDefault(a => a.ID == id);
        }

        public Course GetSingleCourseWithMarks(int id)
        {
            return _context.Course
                .Include(x => x.Marks)
                .FirstOrDefault(a => a.ID == id);
        }

        public List<string> GetCoursesTitlesByTeacher(string id)
        {
            return _context.Course.Where(x => x.TeacherID == id).Select(x => x.Title).ToList();
        }

        public async Task UpdateCourseSchedule(Course course, IList<CourseLesson> lessons)
        {
            foreach (var selectedLesson in lessons.Where(x=>x.isBusy))
            {
                var index = course.LessonSchedule.FindIndex(x => x.StartLessonTime == selectedLesson.StartLessonTime && x.WeekDay == selectedLesson.WeekDay);
                course.LessonSchedule[index].isBusy = true;
                course.LessonSchedule[index].BusyExpireDate = DateTime.Now.AddDays(28);
            }
            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCourse(CreateCourseViewModel courseVM,ApplicationUser user) 
        {
            var scheduler = new FormingScheduleService();

            courseVM.Course.TeacherID = user.Id;
            //  courseVM.Course.Teacher = user;
            courseVM.Course.TeacherInfo = new CourseTeacherInfo() { Skype = user.Skype, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName };
            courseVM.Course.ReleaseDate = DateTime.Now;
            courseVM.Course.IsActive = true;
            foreach (var day in courseVM.WeekDays)
            {
                if (day.IsWorkDay)
                {
                    courseVM.Course.WeekPlans.Add(day);
                }
            }

            courseVM.Course.LessonSchedule = scheduler.FormSchedule(courseVM.Course.WeekPlans, courseVM.Course.Duration, DateTime.Now, 1);

            _context.Add(courseVM.Course);
            await _context.SaveChangesAsync();
        }

        public async Task ReleaseLesson(Course course,CourseLesson lesson)
        {
            var index = course.LessonSchedule.FindIndex(x => x.isBusy && x.StartLessonTime.Hour == lesson.StartLessonTime.Hour && x.StartLessonTime.Minute == lesson.StartLessonTime.Minute && x.WeekDay == lesson.WeekDay);
            course.LessonSchedule[index].BusyExpireDate = new DateTime(1,1,1);
            course.LessonSchedule[index].isBusy = false;

            _context.Update(course);
            await _context.SaveChangesAsync();
        }
        
      
    }
}
