using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.TeachersViewModels;

namespace TeachMe.Controllers
{
    public class TeachersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
      


        public TeachersController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            //if (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null)
            //{
            //    //  userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //}
            _manager = manager;
            _context = context;
          
        }
        public IActionResult Index()
        {
            var userList = _context.Users.Include(x=>x.CreatedCourses).Select(x=>x).Where(x=>x.IsTeacher).OrderBy(x=>x.FinalRating).ToList();
            userList.Reverse();
            return View(new TeacherIndexViewModel() { UserList = userList });
        }

        public async Task<IActionResult> TeacherProfile()
        {
            var user = await _manager.GetUserAsync(User);
            return View(user);
        }


    }
}