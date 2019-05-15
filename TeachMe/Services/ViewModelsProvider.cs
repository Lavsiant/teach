using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Data;

using TeachMe.Models;
using TeachMe.Models.AccountViewModels;
using TeachMe.Models.CourseModels;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Models.ManageViewModels;
using TeachMe.Repositories;

namespace TeachMe.Services
{
    public class ViewModelsProvider
    {
        public ViewModelsProvider()
        {

        }

        public ProfileViewModel GetProfileViewModel(ApplicationUser profileUser, ApplicationDbContext _context, ApplicationUser currentSessionUser) 
        {
            var courseRepository = new CourseRepository(_context);
            var userRepository = new UserRepository(_context);
            var ratingService = new FeedbackService();

            var currentU = userRepository.GetUserWithStudentsCoursesList(currentSessionUser.Id);
            var teacher = userRepository.GetTeacherUser(profileUser.Id);

            var profileVM = new ProfileViewModel();
            profileVM.User = teacher;
            profileVM.Subjects = userRepository.GetLeadingSubjectListByTeacherId(profileUser.Id);
            profileVM.CourseTittles = courseRepository.GetCoursesTitlesByTeacher(profileUser.Id);
            profileVM.isReadyForRate = ratingService.CheckIfProfileRaterValid(userRepository.GetUserWithLessonsList(currentSessionUser.Id), profileUser);
            profileVM.isReadyForComment = ratingService.CheckIfCommentatorValid(currentU, teacher);
            profileVM.IsReadyForCreate = ratingService.CheckIfReadyForCreate(profileUser, _context);
            profileVM.isShowCertificates = currentU.Certificats.Count > 0;
            return profileVM;
        }

        public SubscribeViewModel GetSubscribeViewModel(Course course,ApplicationDbContext context,string currentUserId)
        {
            var vm = new SubscribeViewModel();

            var workDays = new List<DayOfWeek>();
            foreach (var item in course.WeekPlans.OrderBy(x => x.WeekDay))
            {
                workDays.Add(item.WeekDay);
            }
            vm.WorkDays = workDays;

            vm.Lessons = new FormingScheduleService().GetAvalibleLessons(context, course, currentUserId);
           // vm.Lessons = new FormingScheduleService().GetAvalibleLessonsForTeacher(context, course, currentUserId);
            vm.Course = course;
            vm.LessonNumber = course.LessonsNumber;

            return vm;
        }

        public HomeViewModel GetHomeViewModel(ApplicationDbContext context)
        {
            var newestList = context.Course.Include(x=>x.TeacherInfo).OrderBy(x => x.ReleaseDate).ToList();
            var bestCourses = context.Course.Include(x => x.TeacherInfo).OrderBy(x => x.FinalRating).ToList();
            var bestTeacher = context.Users.Where(x => x.IsTeacher).OrderBy(x => x.FinalRating).ToList();
            bestTeacher.Reverse();
            newestList.Reverse();
            bestCourses.Reverse();
            //  bestTeacher.Reverse();
            return new HomeViewModel() { NewCourses = newestList.Take(4).ToList(), BestCourses = bestCourses.Take(4).ToList(), BestTeachers = bestTeacher.Take(6).ToList() };
        }

        public IndexCourseViewModel GetIndexCourseViewModel(List<Course> courses, ApplicationDbContext context, ApplicationUser user)
        {
            var categoryQuery = Enum.GetNames(typeof(CourseCategory));
            var sortCriteriaQuery = Enum.GetNames(typeof(CourseSortCriteria));
            var courseIndexVM = new IndexCourseViewModel();
          //  var currentUser = context.Users.Include(x=>x.Certificats).Include(x=>x.Stream).FirstOrDefault(x => x.Id == user.Id);

            courseIndexVM.categories = new SelectList(categoryQuery);
            courseIndexVM.sortCrtiteriaList = new SelectList(sortCriteriaQuery);
            courseIndexVM.Courses = courses;
            //courseIndexVM.User = currentUser;

            return courseIndexVM;
        }

        public SheduleViewModel GetSheduleViewModel(List<UserCourse> studentLessons, List<UserCourse> teacherLessons,string id)
        {
            
            var vm = new SheduleViewModel() { StudentLessons = studentLessons.OrderBy(x => x.WeekDay).ThenBy(x => x.StartLessonTime).ToList(), TeacherLesson = teacherLessons.OrderBy(x => x.WeekDay).ThenBy(x => x.StartLessonTime).ToList(), UserId = id };
            return vm;
        }
    }
}
