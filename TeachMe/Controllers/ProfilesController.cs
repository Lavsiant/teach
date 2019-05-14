using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.UserModel;
using TeachMe.Models;
using TeachMe.Models.AccountViewModels;
using TeachMe.Services;
using TeachMe.Services.Interfaces;

namespace TeachMe.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly FeedbackService _ratingService;
        private readonly IViewModelProvider _vmProvider;
        private readonly IUserService _userService;
        private RepositoryContext _db;

        public ProfilesController(UserManager<ApplicationUser> manager,IUserService us, IViewModelProvider vm, RepositoryContext db)
        {
            _manager = manager;
            _ratingService = new FeedbackService();
            _vmProvider = vm;
            _userService = us;
            _db = db;
        }

        [Authorize]
        public async Task<IActionResult> Index(string id)
        {

            var profile = await _userService.GetUserWithLessonsListAndMarks(id);
           
          
           var currentUser = await _manager.GetUserAsync(User);
            
            
            return View(await _vmProvider.GetProfileViewModel(profile,currentUser));

        }

        public async Task<IActionResult> Comment(ProfileViewModel profileViewModel)
        {
            
            var currentUser = await _manager.GetUserAsync(User);

            var teacher = await _userService.GetUserWithComments(profileViewModel.User.Id);

            if (!String.IsNullOrEmpty(profileViewModel.CommentText))
            {
                var comment = new Comment()
                {
                    CommentatorFullName = $"{currentUser.FirstName} {currentUser.LastName}",
                    CommentatorId = currentUser.Id,
                    Date = DateTime.Now,
                    Text = profileViewModel.CommentText
                };
                teacher.Comments.Add(comment);
                _db.Update(teacher);
                await _db.SaveChangesAsync();
               
               
            }

            var vm = await _vmProvider.GetProfileViewModel(await _userService.GetUserWithLessonsListAndMarks(teacher.Id), currentUser);
            vm.CommentText = "";
            return View("../Profiles/Index",vm);
        }
   
        public async Task<IActionResult> RateProfile(string id, int mark)
        {
            var currentUser = await _manager.GetUserAsync(User);
            var teacher = await _userService.GetUserWithMarks(id);

            var ratingService = new FeedbackService();           
            
            await _manager.UpdateAsync(ratingService.RateTeacher(mark,currentUser.Id,teacher));

            return View("../Profiles/Index",await _vmProvider.GetProfileViewModel(teacher,currentUser));
        }


          public async Task<IActionResult> Commennt(ProfileViewModel profileViewModel)
        {
            
            var currentUser = await _manager.GetUserAsync(User);

            var teacher = await _userService.GetUserWithComments(profileViewModel.User.Id);

            if (!String.IsNullOrEmpty(profileViewModel.CommentText))
            {
                var comment = new Comment()
                {
                    CommentatorFullName = $"{currentUser.FirstName} {currentUser.LastName}",
                    CommentatorId = currentUser.Id,
                    Date = DateTime.Now,
                    Text = profileViewModel.CommentText
                };
                teacher.Comments.Add(comment);
                _db.Update(teacher);
                await _db.SaveChangesAsync();
               
               
            }

            var vm = await _vmProvider.GetProfileViewModel(await _userService.GetUserWithLessonsListAndMarks(teacher.Id), currentUser);
            vm.CommentText = "";
            return View("../Profiles/Index",vm);
        }
    }
}

