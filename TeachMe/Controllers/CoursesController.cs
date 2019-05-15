using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeachMe.Data;
using TeachMe.Models;
using TeachMe.Models.AccountViewModels;
using TeachMe.Models.CourseModels;
using TeachMe.Models.CourseModels.CourseViewModels;
using TeachMe.Repositories;
using TeachMe.Services;
using TeachMe.Services.FilterServices;

namespace TeachMe.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ViewModelsProvider _vmProvider;
        private readonly CourseRepository _courseRepository;
        private readonly UserRepository _userRepository;


        public CoursesController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
         
            _manager = manager;
            _context = context;
            _courseRepository = new CourseRepository(context);
            _userRepository = new UserRepository(context);

            _vmProvider = new ViewModelsProvider();

        }

        // GET: Courses
        [HttpGet]

        public async Task<IActionResult> Index(string searchString, string courseSubject, string courseCategory, string sortCriteria)
        {
            var filterService = new CourseFilterService();

            var courses = filterService.FilterCourseList(_context, searchString, courseSubject, courseCategory, sortCriteria);
            var user = await _manager.GetUserAsync(User);
            var indexVM = _vmProvider.GetIndexCourseViewModel(courses, _context, user);

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

            var course = _courseRepository.GetSingleFullCourse(id);
            var teacher = await _manager.FindByIdAsync(course.TeacherID);
            var currentUser = await _manager.GetUserAsync(User);

            if (course == null || teacher == null)
            {
                return NotFound();
            }

            bool raterMarker = ratingService.CheckIfCourseRaterValid(_manager.GetUserId(User),_context,course);
            bool subscribeMarker = ratingService.CheckIfValidToSubscribe(course.Title, _userRepository.GetUserWithLessonsList(currentUser.Id));
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
                var user = _userRepository.GetUserWithLessonsListAndCreatedCourses(helper.Id);
                user.IsTeacher = true;
                user.CreatedCourses.Add(new CString() { value = courseVM.Course.Title });           
                await _manager.UpdateAsync(user);

                await _courseRepository.CreateCourse(courseVM, user);

                return RedirectToAction(nameof(Index));
            }
            return View(courseVM.Course);
        }


        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.SingleOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updCourse = _context.Course.FirstOrDefault(x => x.ID == course.ID);
                    updCourse.Description = course.Description;
                    updCourse.Title = course.Title;
                    updCourse.Price = course.Price;
                    updCourse.LessonsNumber = course.LessonsNumber;
                    _context.Update(updCourse);
                    await _context.SaveChangesAsync();
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

            var course = _courseRepository.GetSingleFullCourse(id);

            if (course == null)
            {
                return NotFound();
            }
       

            var userHelper = await _manager.GetUserAsync(User);

            
            return View(_vmProvider.GetSubscribeViewModel(course, _context, userHelper.Id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(int? id, SubscribeViewModel subscribeViewModel)
        {
            var course = _courseRepository.GetSingleCourseWithSchedule(subscribeViewModel.Course.ID);

            await _courseRepository.UpdateCourseSchedule(course, subscribeViewModel.Lessons);



            var user = await _manager.GetUserAsync(User);



            var student = _userRepository.GetUserWithLessonsListAndStudentCourses(user.Id);
            var teacher = _userRepository.GetUserWithLessonsList(subscribeViewModel.Course.TeacherID);

            await _userRepository.AddSubscribedLessons(student, teacher, subscribeViewModel.Lessons, course, _manager);


            // var teacher = await _manager.FindByIdAsync(subscribeViewModel.Course.TeacherID);
            teacher.SummaryStudentsNumber++;
            await _manager.UpdateAsync(teacher);

            student.StudentCourses.Add(new CString() { value = course.Title });
            await _manager.UpdateAsync(student);

            course.SummaryStudentsNumber++;
            _context.Update(course);

            var es = new EmailSender();
          // await es.SendEmailAsync(course.TeacherInfo.Email, "course enroll", "on your course enrolled");

            return RedirectToAction("Payment","Courses",new { price = course.Price });
        }


        [HttpGet]
        public async Task<IActionResult> Payment(decimal price)
        {



            return View(new PayViewModel() { Price = price});
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PayViewModel p)
        {




            return RedirectToAction("PaymentSuccess");
        }

        [HttpGet]
        public async Task<IActionResult> PaymentSuccess(PayViewModel p)
        {




            return View();
        }

        public async Task<IActionResult> RateCourse(int id, string mark)
        {
            var course = _courseRepository.GetSingleCourseWithMarks(id);
            var user = await _manager.GetUserAsync(User);
            var ratingService = new FeedbackService();
            await _courseRepository.UpdateCourse(ratingService.RateCourse(mark, user.Id, course));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SearchByName(string searchString)
        {
            var courses = _courseRepository.GetCourseListWithTeacherInfo();
            var filterService = new CourseFilterService();
            courses = filterService.FilterCourseList(courses, searchString, null, null, null);     

            return View("../Courses/Index", _vmProvider.GetIndexCourseViewModel(courses.ToList(),_context,await _manager.GetUserAsync(User)));
        }

        [Authorize]
        public async Task<IActionResult> FinalSearchByName(string searchString)
        {
            Course course = new Course();
            if (!String.IsNullOrEmpty(searchString))
            {
                course = _context.Course.Include(x => x.TeacherInfo).Include(x => x.Marks).Include(y => y.WeekPlans).Include(x => x.Duration).FirstOrDefault(x => x.Title.Equals(searchString));
            }
            var ratingService = new FeedbackService();
            var currentUser = await _manager.GetUserAsync(User);
            bool raterMarker = ratingService.CheckIfCourseRaterValid(_manager.GetUserId(User), _context, course);
            bool subscribeMarker = ratingService.CheckIfValidToSubscribe(course.Title, _userRepository.GetUserWithLessonsList(currentUser.Id));

            return View("../Courses/Details", new DetailsViewModel() { Course = course,isReadtForRate = raterMarker, IsValidForSubscribe = subscribeMarker });


        }

        public async Task<IActionResult> SearchBySubject(string subject)
        {
            var courses = _courseRepository.GetCourseListWithTeacherInfo();
            var filterService = new CourseFilterService();
            courses = courses.Where(x => x.Subject.Equals(subject)).ToList();


            return View("../Courses/Index", _vmProvider.GetIndexCourseViewModel(courses.ToList(), _context, await _manager.GetUserAsync(User)));
        }

        [Authorize]
        public async Task<IActionResult> OpenTeacherProfile(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            var profileVM = new ProfileViewModel() { User = user, Rating = 1 };
            return View("../Profiles/Index", profileVM);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .SingleOrDefaultAsync(m => m.ID == id);
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
            var course = await _context.Course.SingleOrDefaultAsync(m => m.ID == id);
            course.IsActive = false;
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Recovery(int id)
        {
            var course = await _context.Course.SingleOrDefaultAsync(m => m.ID == id);
            course.IsActive = true;
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.ID == id);
        }
    }


}
