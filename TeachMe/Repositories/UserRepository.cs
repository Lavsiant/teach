using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.CourseModels;

namespace TeachMe.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser GetUserWithLessonsListAndMarks(string id)
        {
            return _context.Users.Include(x => x.Marks).Include(x=>x.Certificats).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithLessonsList(string id)
        {
            return _context.Users.Include(x => x.LessonsList).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithStudentsCoursesList(string id)
        {
            return _context.Users.Include(x => x.StudentCourses).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithLessonsListAndStudentCourses(string id)
        {
            return _context.Users.Include(x => x.LessonsList).Include(x=>x.StudentCourses).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithComments(string id)
        {
            return _context.Users.Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetTeacherUser(string id)
        {
            return _context.Users.Include(x => x.StudentCourses).Include(x => x.Stream).Include(x => x.Comments).Include(x => x.CreatedCourses).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithStreamInfo(string id)
        {
            return _context.Users.Include(x => x.Stream).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithLessonsListAndCreatedCourses(string id)
        {
            return _context.Users.Include(x => x.LessonsList).Include(x => x.CreatedCourses).FirstOrDefault(x => x.Id == id);
        }

        public ApplicationUser GetUserWithCertificates(string id)
        {
            return _context.Users.Include(x => x.Certificats).FirstOrDefault(x => x.Id == id);
        }

        public async Task AddSubscribedLessons(ApplicationUser student, ApplicationUser teacher, IList<CourseLesson> lessons, Course course, UserManager<ApplicationUser> manager)
        {

            // var teacher = await _manager.FindByIdAsync(subscribeViewModel.Course.TeacherID);
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

            await manager.UpdateAsync(student);
            await manager.UpdateAsync(teacher);
        }



        public ApplicationUser GetUserWithMarks(string id)
        {
           return _context.Users.Include(x => x.Marks).FirstOrDefault(x => x.Id == id);
        }

        public async Task DeleteUserCourse(DayOfWeek day,DateTime startLessonTime,string courseTittle)
        {
            var userList = _context.Users.Include(x => x.LessonsList).ToList();

            foreach (var user in userList)
            {
                foreach (var lesson in user.LessonsList)
                {
                    if(lesson.StartLessonTime == startLessonTime && lesson.WeekDay == day && lesson.CourseTittle == courseTittle)
                    {
                        var index = user.LessonsList.FindIndex(x => x.CourseTittle == courseTittle && x.WeekDay == day && x.StartLessonTime == startLessonTime);
                        
                        user.LessonsList.RemoveAt(index);
                        _context.Update(user);
                       
                        await _context.SaveChangesAsync();

                        break;
                    }
                }
            }
        }

        public async Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle, string id)
        {
            var user = _context.Users.Include(x => x.LessonsList).FirstOrDefault(x=>x.Id==id);

            foreach (var item in user.LessonsList)
            {
                if(item.WeekDay == day && item.StartLessonTime.Hour == startLessonTime.Hour && item.StartLessonTime.Minute==startLessonTime.Minute && item.CourseTittle == courseTittle)
                {
                    var index = user.LessonsList.FindIndex(x=>x.WeekDay == day && x.StartLessonTime.Hour == startLessonTime.Hour && x.StartLessonTime.Minute == startLessonTime.Minute && x.CourseTittle == courseTittle);
                    user.LessonsList.RemoveAt(index);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    break;
                }
            }  
        }

        public List<string> GetLeadingSubjectListByTeacherId(string id)
        {
            return _context.Course.Where(x => x.TeacherID == id).Select(x => x.Subject).ToList();
        }
    }
}
