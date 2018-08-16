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


    }
}
