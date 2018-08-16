using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models.CourseModels.CourseViewModels;

namespace TeachMe.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetCourseListWithTeacherInfo();

        Task<List<Course>> GetCourseListWithLessonSchedule();

        Task<Course> GetSingleFullCourse(int? id);

        Task<Course> GetCourseWithLessonsByTittle(string tittle);

        Task<Course> GetSingleCourseWithSchedule(int id);

        Task<Course> GetSingleCourseWithMarks(int id);

        Task<List<string>> GetCoursesTitlesByTeacher(string id);

        Task UpdateCourseSchedule(Course course, IList<CourseLesson> lessons);

        Task UpdateCourse(Course course);

        //public async Task CreateCourse(CreateCourseViewModel courseVM, ApplicationUser user)

        Task ReleaseLesson(Course course, CourseLesson lesson);

        Task<Course> FindCourseByName(string name);

        Task CreateCourse(CreateCourseViewModel courseVM, ApplicationUser user);

    }
}
