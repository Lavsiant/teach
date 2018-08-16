using DBRepository.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models.AccountViewModels;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Models.ManageViewModels;
using TeachMe.Services.Interfaces;

namespace TeachMe.Services.Implementations
{
    public class ViewModelProvider : IViewModelProvider
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public ViewModelProvider(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        public async Task<ProfileViewModel> GetProfileViewModel(ApplicationUser profileUser, ApplicationUser currentSessionUser)
        {
            var ratingService = new FeedbackService();

            var currentU = await _userService.GetUserWithStudentsCoursesList(currentSessionUser.Id);
            var teacher = await _userService.GetTeacherUser(profileUser.Id);

            var profileVM = new ProfileViewModel();
            profileVM.User = teacher;
            profileVM.Subjects = await _userService.GetLeadingSubjectListByTeacherId(profileUser.Id);
            profileVM.CourseTittles = await _courseService.GetCoursesTitlesByTeacher(profileUser.Id);
            profileVM.isReadyForRate = ratingService.CheckIfProfileRaterValid(await _userService.GetUserWithLessonsList(currentSessionUser.Id), profileUser);
            profileVM.isReadyForComment = ratingService.CheckIfCommentatorValid(currentU, teacher);
            profileVM.IsReadyForCreate = ratingService.CheckIfReadyForCreate(profileUser);
            profileVM.isShowCertificates = currentU.Certificats.Count > 0;
            return profileVM;
        }

        public async Task<SubscribeViewModel> GetSubscribeViewModel(Course course, string currentUserId)
        {
            var vm = new SubscribeViewModel();

            var workDays = new List<DayOfWeek>();
            foreach (var item in course.WeekPlans.OrderBy(x => x.WeekDay))
            {
                workDays.Add(item.WeekDay);
            }
            vm.WorkDays = workDays;

            vm.Lessons = new FormingScheduleService().GetAvalibleLessons(await _userService.GetUserWithLessonsList(currentUserId), course, currentUserId);
            // vm.Lessons = new FormingScheduleService().GetAvalibleLessonsForTeacher(context, course, currentUserId);
            vm.Course = course;

            return vm;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var courseList1 = await _courseService.GetCourseListWithTeacherInfo();
            var newestList = courseList1.OrderBy(x => x.ReleaseDate).ToList();

            courseList1 = await _courseService.GetCourseListWithTeacherInfo();
            var bestCourses = courseList1.OrderBy(x => x.FinalRating).ToList();

            var userList = await _userService.GetUsers();
            var bestTeacher = userList.Where(x => x.IsTeacher).OrderBy(x => x.FinalRating).ToList();
            bestTeacher.Reverse();
            newestList.Reverse();
            bestCourses.Reverse();
            
            return new HomeViewModel() { NewCourses = newestList.Take(4).ToList(), BestCourses = bestCourses.Take(4).ToList(), BestTeachers = bestTeacher.Take(6).ToList() };
        }

        public async Task<IndexCourseViewModel> GetIndexCourseViewModel(List<Course> courses, ApplicationUser user)
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

        public SheduleViewModel GetSheduleViewModel(List<UserCourse> studentLessons, List<UserCourse> teacherLessons, string id)
        {
            var vm = new SheduleViewModel() { StudentLessons = studentLessons.OrderBy(x => x.WeekDay).ThenBy(x => x.StartLessonTime).ToList(), TeacherLesson = teacherLessons.OrderBy(x => x.WeekDay).ThenBy(x => x.StartLessonTime).ToList(), UserId = id };
            return vm;
        }
    }
}
