using Model.CourseModel;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Models.AccountViewModels;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Models.ManageViewModels;

namespace TeachMe.Services.Interfaces
{
    public interface IViewModelProvider
    {
        Task<ProfileViewModel> GetProfileViewModel(ApplicationUser profileUser, ApplicationUser currentSessionUser);

        Task<SubscribeViewModel> GetSubscribeViewModel(Course course, string currentUserId);

        Task<HomeViewModel> GetHomeViewModel();

        Task<IndexCourseViewModel> GetIndexCourseViewModel(List<Course> courses, ApplicationUser user);

        SheduleViewModel GetSheduleViewModel(List<UserCourse> studentLessons, List<UserCourse> teacherLessons, string id);
    }
}
