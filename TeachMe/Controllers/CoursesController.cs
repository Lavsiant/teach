using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.CourseModel;
using Model.UserModel;
using TeachMe.Models;
using TeachMe.Models.AccountViewModels;
using TeachMe.Models.CourseModels;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Services;
using TeachMe.Services.FilterServices;
using TeachMe.Services.Implementations;
using TeachMe.Services.Interfaces;

namespace TeachMe.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IViewModelProvider _vmProvider;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;

        public CoursesController( UserManager<ApplicationUser> manager,IViewModelProvider vmprovider, IUserService userService,ICourseService courseService)
        {
         
            _manager = manager;
            _vmProvider = vmprovider;
            _userService = userService;
            _courseService = courseService;          
        }

        // GET: Courses
        [HttpGet]

        public async Task<IActionResult> Index(string searchString, string courseSubject, string courseCategory, string sortCriteria)
        {
            var filterService = new CourseFilterService();
            var courses = await _courseService.GetCourseListWithTeacherInfo();
            courses = courses.Where(x => x.IsActive).ToList();
            courses = filterService.FilterCourseList(courses, searchString, courseSubject, courseCategory, sortCriteria);
            var user = await _manager.GetUserAsync(User);
            var indexVM = await _vmProvider.GetIndexCourseViewModel(courses, user);

            return View(indexVM);
        }

        // GET: Courses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
         
            var ratingService = new FeedbackService();

            var course =await _courseService.GetSingleFullCourse(id);
            var teacher = await _manager.FindByIdAsync(course.TeacherID);
            var currentUser = await _manager.GetUserAsync(User);

            if (course == null || teacher == null)
            {
                return NotFound();
            }

            bool raterMarker = ratingService.CheckIfCourseRaterValid(_manager.GetUserId(User),await _userService.GetUserWithLessonsList(_manager.GetUserId(User)),course);
            bool subscribeMarker = ratingService.CheckIfValidToSubscribe(course.Title,await _userService.GetUserWithLessonsList(currentUser.Id));
            return View(new DetailsViewModel() { Course = course, Teacher = teacher, isReadtForRate = raterMarker, IsValidForSubscribe = subscribeMarker});
        }

        // GET: Courses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateCourseViewModel());
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateCourseViewModel courseVM)
        {
            if (ModelState.IsValid)
            {
                var helper = await _manager.GetUserAsync(User);
                var user = await _userService.GetUserWithLessonsListAndCreatedCourses(helper.Id);
                user.IsTeacher = true;
                user.CreatedCourses.Add(courseVM.Course);           
                

                await _courseService.CreateCourse(courseVM, user);

                return RedirectToAction(nameof(Index));
            }
            return View(courseVM.Course);
        }


        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
    

            var course = await _courseService.GetSingleCourseWithMarks(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Type,Price,LessonsNumber")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _courseService.UpdateCourse(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Subscribe/5

        [Authorize]
        public async Task<IActionResult> Subscribe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetSingleFullCourse(id);

            if (course == null)
            {
                return NotFound();
            }
       

            var userHelper = await _manager.GetUserAsync(User);

            
            return View(await _vmProvider.GetSubscribeViewModel(course, userHelper.Id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(int? id, SubscribeViewModel subscribeViewModel)
        {
            var course = await _courseService.GetSingleCourseWithSchedule(subscribeViewModel.Course.ID);

            await _courseService.UpdateCourseSchedule(course, subscribeViewModel.Lessons);



            var user = await _manager.GetUserAsync(User);

            var student = await _userService.GetUserWithLessonsListAndStudentCourses(user.Id);
            var teacher = await _userService.GetUserWithLessonsList(subscribeViewModel.Course.TeacherID);

            await _userService.AddSubscribedLessons(student, teacher, subscribeViewModel.Lessons, course, _manager);


            // var teacher = await _manager.FindByIdAsync(subscribeViewModel.Course.TeacherID);
            teacher.SummaryStudentsNumber++;
            await _manager.UpdateAsync(teacher);

            student.StudentCourses.Add(new CString() { value = course.Title });
            await _manager.UpdateAsync(student);

            course.SummaryStudentsNumber++;
            await _courseService.UpdateCourse(course);

            var es = new EmailSender();
          // await es.SendEmailAsync(course.TeacherInfo.Email, "course enroll", "on your course enrolled");

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> RateCourse(int id, string mark)
        {
            var course = await _courseService.GetSingleCourseWithMarks(id);
            var user = await _manager.GetUserAsync(User);
            var ratingService = new FeedbackService();
            await _courseService.UpdateCourse(ratingService.RateCourse(mark, user.Id, course));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SearchByName(string searchString)
        {
            var courses =await _courseService.GetCourseListWithTeacherInfo();
            var filterService = new CourseFilterService();
            courses = filterService.FilterCourseList(courses, searchString, null, null, null);     

            return View("../Courses/Index",await _vmProvider.GetIndexCourseViewModel(courses.ToList(),await _manager.GetUserAsync(User)));
        }

        [Authorize]
        public async Task<IActionResult> FinalSearchByName(string searchString)
        {
            Course course = new Course();
            if (!String.IsNullOrEmpty(searchString))
            {
                course = await _courseService.FindCourseByName(searchString);
            }
            var ratingService = new FeedbackService();
            var currentUser = await _manager.GetUserAsync(User);
            bool raterMarker = ratingService.CheckIfCourseRaterValid(_manager.GetUserId(User), await _userService.GetUserWithLessonsList(_manager.GetUserId(User)), course);
            bool subscribeMarker = ratingService.CheckIfValidToSubscribe(course.Title,await _userService.GetUserWithLessonsList(currentUser.Id));

            return View("../Courses/Details", new DetailsViewModel() { Course = course,isReadtForRate = raterMarker, IsValidForSubscribe = subscribeMarker });


        }

        public async Task<IActionResult> SearchBySubject(string subject)
        {
            var courses = await _courseService.GetCourseListWithTeacherInfo();
            var filterService = new CourseFilterService();
            courses = courses.Where(x => x.Subject.Equals(subject)).ToList();


            return View("../Courses/Index",await _vmProvider.GetIndexCourseViewModel(courses.ToList(), await _manager.GetUserAsync(User)));
        }

        [Authorize]
        public async Task<IActionResult> OpenTeacherProfile(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            var profileVM = new ProfileViewModel() { User = user, Rating = 1 };
            return View("../Profiles/Index", profileVM);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
     

            var course = await _courseService.GetSingleCourseWithMarks(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseService.GetSingleCourseWithMarks(id);
            course.IsActive = false;

      
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Recovery(int id)
        {
            var course = await _courseService.GetSingleCourseWithMarks(id);
            course.IsActive = true;
            await _courseService.UpdateCourse(course);
            //_context.Update(course);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return true;
        }
    }
}
