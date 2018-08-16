using DBRepository.Interfaces;
using DBRepository.Repositories;
using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Services.Interfaces;

namespace TeachMe.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository context)
        {
            _courseRepository = context;
        }


        public async Task<List<Course>> GetCourseListWithTeacherInfo()
        {
            return await _courseRepository.GetCourseListWithTeacherInfo();
        }

        public async Task<List<Course>> GetCourseListWithLessonSchedule()
        {
            return await _courseRepository.GetCourseListWithLessonSchedule();

        }

        public async Task<Course> GetSingleFullCourse(int? id)
        {

            return await _courseRepository.GetSingleFullCourse(id);
        }

        public async Task<Course> GetCourseWithLessonsByTittle(string tittle)
        {
            return await _courseRepository.GetCourseWithLessonsByTittle(tittle);
        }

        public async Task<Course> GetSingleCourseWithSchedule(int id)
        {
            return await _courseRepository.GetSingleCourseWithSchedule(id);
        }

        public async Task<Course> GetSingleCourseWithMarks(int id)
        {
            return await _courseRepository.GetSingleCourseWithMarks(id);
        }

        public async Task<List<string>> GetCoursesTitlesByTeacher(string id)
        {
            return await _courseRepository.GetCoursesTitlesByTeacher(id);
        }

        public async Task UpdateCourseSchedule(Course course, IList<CourseLesson> lessons)
        {
            await _courseRepository.UpdateCourseSchedule(course, lessons);
        }

        public async Task UpdateCourse(Course course)
        {
            await _courseRepository.UpdateCourse(course);
        }

        public async Task CreateCourse(CreateCourseViewModel courseVM, ApplicationUser user)
        {
            var scheduler = new FormingScheduleService();

            courseVM.Course.TeacherID = user.Id;
           // courseVM.Course.Tea = user;
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

            await _courseRepository.CreateCourse(courseVM.Course,user);
        }

        public async Task<Course> FindCourseByName(string name)
        {
           return await _courseRepository.FindCourseByName(name);
        }

        public async Task ReleaseLesson(Course course, CourseLesson lesson)
        {
            await _courseRepository.ReleaseLesson(course, lesson);
    
            //using (var context = ContextFactory.CreateDbContext(ConnectionString))
            //{
            //    var index = course.LessonSchedule.FindIndex(x => x.isBusy && x.StartLessonTime.Hour == lesson.StartLessonTime.Hour && x.StartLessonTime.Minute == lesson.StartLessonTime.Minute && x.WeekDay == lesson.WeekDay);
            //    course.LessonSchedule[index].BusyExpireDate = new DateTime(1, 1, 1);
            //    course.LessonSchedule[index].isBusy = false;

            //    context.Update(course);
            //    await context.SaveChangesAsync();
            //}
        }
    }
}

