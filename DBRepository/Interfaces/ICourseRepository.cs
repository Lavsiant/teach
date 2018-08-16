using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    public interface ICourseRepository
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

        Task CreateCourse(Course course, ApplicationUser user);

        Task ReleaseLesson(Course course, CourseLesson lesson);

        Task<Course> FindCourseByName(string name);
    }
}
