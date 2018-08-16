using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.UserModel;
using TeachMe.Models;
using TeachMe.Models.TeachersViewModels;

namespace TeachMe.Controllers
{
    public class TeachersController : Controller
    {

        private readonly RepositoryContext _context;
        private readonly UserManager<ApplicationUser> _manager;     


        public TeachersController(RepositoryContext context, UserManager<ApplicationUser> manager)
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
            var userList = _context.Users.Include(x=>x.CreatedCourses).Where(x=>x.IsTeacher).OrderBy(x=>x.FinalRating).ToList();
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