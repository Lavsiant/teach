using DBRepository.Interfaces;
using Microsoft.AspNetCore.Identity;
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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, IRepositoryContextFactory contextFactory) : base(connectionString, contextFactory) { }


        public async Task<ApplicationUser> GetUserWithLessonsListAndMarks(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.Marks).Include(x => x.Certificats).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ApplicationUser> GetUserWithLessonsList(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.LessonsList).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ApplicationUser> GetUserWithStudentsCoursesList(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.StudentCourses).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ApplicationUser> GetUserWithLessonsListAndStudentCourses(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.LessonsList).Include(x => x.StudentCourses).FirstOrDefaultAsync(x => x.Id == id);

            }
        }

        public async Task<ApplicationUser> GetUserWithComments(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ApplicationUser> GetTeacherUser(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.StudentCourses).Include(x => x.Stream).Include(x => x.Comments).Include(x=>x.Certificats).Include(x => x.CreatedCourses).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ApplicationUser> GetUserWithStreamInfo(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.Stream).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<ApplicationUser> GetUserWithLessonsListAndCreatedCourses(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.LessonsList).Include(x => x.CreatedCourses).FirstOrDefaultAsync(x => x.Id == id);

            }
        }

        public async Task<ApplicationUser> GetUserWithCertificates(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.Certificats).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task AddSubscribedLessons(ApplicationUser student, ApplicationUser teacher, IList<CourseLesson> lessons, Course course, string title)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                student = context.Users.Include(x => x.LessonsList).Include(x=>x.StudentCourses).FirstOrDefault(x=>x.Id == student.Id);
                teacher = context.Users.Include(x => x.LessonsList).FirstOrDefault(x => x.Id == teacher.Id);
                teacher.SummaryStudentsNumber++;

                student.StudentCourses.Add(new CString() { value = course.Title });
                foreach (var selectedLesson in lessons.Where(x => x.isBusy))
                {
                    student.LessonsList.Add(new UserCourse()
                    {
                        StartLessonTime = selectedLesson.StartLessonTime,
                        EndLessonTime = selectedLesson.EndLessonTime,
                        CourseTittle = course.Title,
                        ExpireDate = DateTime.Now.AddDays(28),
                        WeekDay = selectedLesson.WeekDay,
                        TeacherId = course.TeacherID
                    });
                    teacher.LessonsList.Add(new UserCourse()
                    {
                        StartLessonTime = selectedLesson.StartLessonTime,
                        EndLessonTime = selectedLesson.EndLessonTime,
                        CourseTittle = course.Title,
                        ExpireDate = DateTime.Now.AddDays(28),
                        WeekDay = selectedLesson.WeekDay,
                        StudentId = student.Id
                    });
                }
                context.Update(student);
                context.Update(teacher);
            }
            // var teacher = await _manager.FindByIdAsync(subscribeViewModel.Course.TeacherID);
          
        }



        public async Task<ApplicationUser> GetUserWithMarks(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.Marks).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var userList = context.Users.Include(x => x.LessonsList).ToList();

                foreach (var user in userList)
                {
                    foreach (var lesson in user.LessonsList)
                    {
                        if (lesson.StartLessonTime == startLessonTime && lesson.WeekDay == day && lesson.CourseTittle == courseTittle)
                        {
                            var index = user.LessonsList.FindIndex(x => x.CourseTittle == courseTittle && x.WeekDay == day && x.StartLessonTime == startLessonTime);

                            user.LessonsList.RemoveAt(index);
                            context.Update(user);

                            await context.SaveChangesAsync();

                            break;
                        }
                    }
                }
            }
        }

        public async Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle, string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var user = context.Users.Include(x => x.LessonsList).FirstOrDefault(x => x.Id == id);

                foreach (var item in user.LessonsList)
                {
                    if (item.WeekDay == day && item.StartLessonTime.Hour == startLessonTime.Hour && item.StartLessonTime.Minute == startLessonTime.Minute && item.CourseTittle == courseTittle)
                    {
                        var index = user.LessonsList.FindIndex(x => x.WeekDay == day && x.StartLessonTime.Hour == startLessonTime.Hour && x.StartLessonTime.Minute == startLessonTime.Minute && x.CourseTittle == courseTittle);
                        user.LessonsList.RemoveAt(index);
                        context.Update(user);
                        await context.SaveChangesAsync();
                        break;
                    }
                }
            }
        }

        public async Task<List<string>> GetLeadingSubjectListByTeacherId(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Course.Where(x => x.TeacherID == id).Select(x => x.Subject).ToListAsync();
            }
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.ToListAsync();
            }
        }

        public string GetTeacherNameById(string id)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var user = context.Users.Find(id);
                return $"{user.FirstName} {user.LastName}";
            }
        }

        public async Task<List<ApplicationUser>> GetUsersWithLessonList()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Users.Include(x => x.LessonsList).ToListAsync();
            }
        }

        public async Task UpdateUserCertificates(string userId, string path)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var user = await context.Users.Include(x => x.Certificats).FirstOrDefaultAsync(x => x.Id == userId);
                user.Certificats.Add(new CString() { value = path });
                context.Update(user);
            }
        }
   
    }

}
