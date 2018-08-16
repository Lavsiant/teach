using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }


        public async Task<List<Course>> GetCourseListWithTeacherInfo()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Include(x => x.TeacherInfo).Select(x => x).ToListAsync();
            }
        }

        public async Task<List<Course>> GetCourseListWithLessonSchedule()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Include(x => x.LessonSchedule).Select(x => x).ToListAsync();
            }
        }

        public async Task<Course> GetSingleFullCourse(int? id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Include(x => x.TeacherInfo)
                .Include(x => x.Marks)
                .Include(y => y.WeekPlans)
                .Include(x => x.Duration)
                .Include(x => x.LessonSchedule)
                .SingleOrDefaultAsync(a => a.ID == id);
            }
        }

        public async Task<Course> GetCourseWithLessonsByTittle(string tittle)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Include(x => x.LessonSchedule).FirstOrDefaultAsync(x => x.Title.Equals(tittle));
            }
        }

        public async Task<Course> GetSingleCourseWithSchedule(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course
                .Include(x => x.LessonSchedule)
                .Include(x => x.TeacherInfo)
                .FirstOrDefaultAsync(a => a.ID == id);
            }
        }

        public async Task<Course> GetSingleCourseWithMarks(int id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course
                .Include(x => x.Marks)
                .FirstOrDefaultAsync(a => a.ID == id);
            }
        }

        public async Task<List<string>> GetCoursesTitlesByTeacher(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Where(x => x.TeacherID == id).Select(x => x.Title).ToListAsync();
            }
        }

        public async Task UpdateCourseSchedule(Course course, IList<CourseLesson> lessons)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                foreach (var selectedLesson in lessons.Where(x => x.isBusy))
                {
                    var index = course.LessonSchedule.FindIndex(x => x.StartLessonTime == selectedLesson.StartLessonTime && x.WeekDay == selectedLesson.WeekDay);
                    course.LessonSchedule[index].isBusy = true;
                    course.LessonSchedule[index].BusyExpireDate = DateTime.Now.AddDays(28);
                }
                context.Update(course);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateCourse(Course course)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Update(course);
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateCourse(Course course,ApplicationUser user)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Course.Add(course);
                context.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task ReleaseLesson(Course course, CourseLesson lesson)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var index = course.LessonSchedule.FindIndex(x => x.isBusy && x.StartLessonTime.Hour == lesson.StartLessonTime.Hour && x.StartLessonTime.Minute == lesson.StartLessonTime.Minute && x.WeekDay == lesson.WeekDay);
                course.LessonSchedule[index].BusyExpireDate = new DateTime(1, 1, 1);
                course.LessonSchedule[index].isBusy = false;

                context.Update(course);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Course> FindCourseByName(string name)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Include(x => x.TeacherInfo).Include(x => x.Marks).Include(y => y.WeekPlans).Include(x => x.Duration).FirstOrDefaultAsync(x => x.Title.Equals(name));
            }

        }
    }
}
