using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Services;

namespace TeachMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _manager = manager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var checker = new UserCourseDateExpireChecker(_context);
            await checker.CheckAllUserCourses();
            var vmProvider = new ViewModelsProvider();

            return View(vmProvider.GetHomeViewModel(_context));
        }

        [HttpGet]
        public async Task<IActionResult> Chat() {

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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
