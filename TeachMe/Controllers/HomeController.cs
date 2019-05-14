using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DBRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.CourseModel;
using Model.UserModel;

using TeachMe.Models;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Services;
using TeachMe.Services.Interfaces;

namespace TeachMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IViewModelProvider _vmProvider;

        public HomeController(UserManager<ApplicationUser> manager, IViewModelProvider viewModelProvider)
        {
            _manager = manager;
            _vmProvider = viewModelProvider;
        }

        public async Task<IActionResult> Index()
        {
         
            return View(await _vmProvider.GetHomeViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Chat()
        {

            var user = await _manager.GetUserAsync(User);
            return View("Chat", $"{user.FirstName} {user.LastName}");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";



            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public bool CheckIfCourseRaterValid(string userId, ApplicationUser user, Course course)
        {

            if (course.TeacherID == userId)
            {
                return false;
            }

            bool raterMarker = false;

            foreach (var item in user.LessonsList)
            {
                if (item.CourseTittle == course.Title)
                {
                    raterMarker = true;
                }
            }
            if (raterMarker)
            {
                foreach (var mark in course.Marks)
                {
                    if (mark.RaterId == userId)
                    {
                        raterMarker = false;
                    }
                }
            }
            return raterMarker;
        }

        public bool CheckIfProfileRaterValid(ApplicationUser rater, ApplicationUser teacher)
        {
            bool raterMarker = false;
            if (rater.Id == teacher.Id)
            {
                return false;
            }

            foreach (var item in rater.LessonsList)
            {
                if (item.TeacherId == teacher.Id)
                {
                    raterMarker = true;
                }
            }

            if (raterMarker)
            {
                foreach (var mark in teacher.Marks)
                {
                    if (mark.RaterId == rater.Id)
                    {
                        raterMarker = false;
                    }
                }
            }
            return raterMarker;
        }

        public bool CheckIfValidToSubscribe(string courseTitle, ApplicationUser user)
        {
            bool marker = true;

            foreach (var item in user.LessonsList)
            {
                if (item.CourseTittle.Equals(courseTitle))
                {
                    marker = false;
                }
            }
            return marker;
        }

        public bool CheckIfCommentatorValid(ApplicationUser commentator,ApplicationUser teacher)
        {
            bool result = false;
            foreach (var item in commentator.StudentCourses)
            {
                foreach (var c in teacher.CreatedCourses)
                {
                    if (item.value.Equals(c.Title))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }


    }

}
