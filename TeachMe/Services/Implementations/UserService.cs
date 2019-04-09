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
using DBRepository.Repositories;
using TeachMe.Services.Interfaces;

namespace TeachMe.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserWithLessonsListAndMarks(string id)
        {
            return await _userRepository.GetUserWithLessonsListAndMarks(id);
        }

        public async Task<ApplicationUser> GetUserWithLessonsList(string id)
        {
            return await _userRepository.GetUserWithLessonsList(id);
        }

        public async Task<ApplicationUser> GetUserWithStudentsCoursesList(string id)
        {
            return await _userRepository.GetUserWithStudentsCoursesList(id);
        }

        public async Task<ApplicationUser> GetUserWithLessonsListAndStudentCourses(string id)
        {
            return await _userRepository.GetUserWithLessonsListAndStudentCourses(id);
        }

        public async Task<ApplicationUser> GetUserWithComments(string id)
        {
            return await _userRepository.GetUserWithComments(id);
        }

        public async Task<ApplicationUser> GetTeacherUser(string id)
        {
            return await _userRepository.GetTeacherUser(id);
        }

        public async Task<ApplicationUser> GetUserWithStreamInfo(string id)
        {
            return await _userRepository.GetUserWithStreamInfo(id);
        }

        public async Task<ApplicationUser> GetUserWithLessonsListAndCreatedCourses(string id)
        {
            return await _userRepository.GetUserWithLessonsListAndCreatedCourses(id);
        }

        public async Task<ApplicationUser> GetUserWithCertificates(string id)
        {
            return await _userRepository.GetUserWithCertificates(id);
        }

        public async Task AddSubscribedLessons(ApplicationUser student, ApplicationUser teacher, IList<CourseLesson> lessons, Course course, string manager)
        {
            await _userRepository.AddSubscribedLessons(student, teacher, lessons, course, manager);
        }

        public async Task<ApplicationUser> GetUserWithMarks(string id)
        {
            return await _userRepository.GetUserWithMarks(id);
        }

        public async Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle)
        {
            await _userRepository.DeleteUserCourse(day, startLessonTime, courseTittle);
        }

        public async Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle, string id)
        {
            await _userRepository.DeleteUserCourse(day, startLessonTime, courseTittle, id);
        }

        public async Task<List<string>> GetLeadingSubjectListByTeacherId(string id)
        {
            return await _userRepository.GetLeadingSubjectListByTeacherId(id);
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public string GetTeacherNameById(string id)
        {
            return _userRepository.GetTeacherNameById(id);
        }

        public async Task<List<ApplicationUser>> GetUsersWithLessonList()
        {
            return await _userRepository.GetUsersWithLessonList(); 
        }

        public async Task UpdateUserCertificates(string userId, string path)
        {
            await _userRepository.UpdateUserCertificates(userId, path);
        }
    }
}

