using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Data;
using TeachMe.Repositories;

namespace TeachMe.Services
{
    public class UserCourseDateExpireChecker
    {
        private readonly CourseRepository _courseRepository;
        private readonly UserRepository _userRepository;

        public UserCourseDateExpireChecker(ApplicationDbContext context)
        {
            _courseRepository = new CourseRepository(context);
            _userRepository = new UserRepository(context);
        }

        public async Task CheckAllUserCourses()
        {
            var courses = _courseRepository.GetCourseListWithLessonSchedule();
            var curDate = DateTime.Now;

            foreach (var course in courses)
            {
                foreach (var  lesson in course.LessonSchedule.Where(x=>x.isBusy))
                {
                    if(lesson.BusyExpireDate <= curDate && lesson.BusyExpireDate.Year!=1)
                    {
                        await _courseRepository.ReleaseLesson(course, lesson);
                        await _userRepository.DeleteUserCourse(lesson.WeekDay, lesson.StartLessonTime, course.Title);
                    }
                }
            }
        }
    }
}
