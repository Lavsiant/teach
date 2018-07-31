using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.AccountViewModels;
using TeachMe.Repositories;
using TeachMe.Services;

namespace TeachMe.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly FeedbackService _ratingService;
        private readonly ViewModelsProvider _vmProvider;
        private readonly UserRepository _userRepository;



        public ProfilesController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _manager = manager;
            _context = context;
            _ratingService = new FeedbackService();
            _vmProvider = new ViewModelsProvider();
            _userRepository = new UserRepository(context);
        }

        [Authorize]
        public async Task<IActionResult> Index(string id)
        {

            var profile = _userRepository.GetUserWithLessonsListAndMarks(id);
           
          
           var currentUser = await _manager.GetUserAsync(User);
            
            
            return View(_vmProvider.GetProfileViewModel(profile,_context,currentUser));

        }

        public async Task<IActionResult> Comment(ProfileViewModel profileViewModel)
        {
            
            var currentUser = await _manager.GetUserAsync(User);

            var teacher = _userRepository.GetUserWithComments(profileViewModel.User.Id);

            if (!String.IsNullOrEmpty(profileViewModel.CommentText))
            {
                var comment = new Comment()
                {
                    CommentatorFullName = $"{currentUser.FirstName} {currentUser.LastName}",
                    CommentatorId = currentUser.Id,
                    Date = DateTime.Now,
                    Text = profileViewModel.CommentText
                }
                ;
                teacher.Comments.Add(comment);
                await _manager.UpdateAsync(teacher);
            }

            var vm = _vmProvider.GetProfileViewModel(_userRepository.GetUserWithLessonsListAndMarks(teacher.Id), _context, currentUser);
            vm.CommentText = "";
            return View("../Profiles/Index",vm);
        }
   
        public async Task<IActionResult> RateProfile(string id, int mark)
        {
            var currentUser = await _manager.GetUserAsync(User);
            var teacher = _userRepository.GetUserWithMarks(id);

            var ratingService = new FeedbackService();           
            
            await _manager.UpdateAsync(ratingService.RateTeacher(mark,currentUser.Id,teacher));

            return View("../Profiles/Index", _vmProvider.GetProfileViewModel(teacher,_context,currentUser));
        }
    }
}

