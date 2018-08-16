﻿using Microsoft.AspNetCore.Identity;
using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeachMe.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserWithLessonsListAndMarks(string id);

        Task<ApplicationUser> GetUserWithLessonsList(string id);

        Task<ApplicationUser> GetUserWithStudentsCoursesList(string id);

        Task<ApplicationUser> GetUserWithLessonsListAndStudentCourses(string id);

        Task<ApplicationUser> GetUserWithComments(string id);

        Task<ApplicationUser> GetTeacherUser(string id);

        Task<ApplicationUser> GetUserWithStreamInfo(string id);

        Task<ApplicationUser> GetUserWithLessonsListAndCreatedCourses(string id);

        Task<ApplicationUser> GetUserWithCertificates(string id);

        Task AddSubscribedLessons(ApplicationUser student, ApplicationUser teacher, IList<CourseLesson> lessons, Course course, UserManager<ApplicationUser> manager);

        Task<ApplicationUser> GetUserWithMarks(string id);

        Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle);

        Task DeleteUserCourse(DayOfWeek day, DateTime startLessonTime, string courseTittle, string id);

        Task<List<string>> GetLeadingSubjectListByTeacherId(string id);

        Task<List<ApplicationUser>> GetUsers();

        string GetTeacherNameById(string id);

        Task<List<ApplicationUser>> GetUsersWithLessonList();
    }
}
