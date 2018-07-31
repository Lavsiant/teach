using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachMe.Data;
using TeachMe.Models.CourseModels;
using TeachMe.Repositories;

namespace TeachMe.Services.FilterServices
{
    public class CourseFilterService
    {
        public List<Course> FilterCourseList(List<Course> courses, string searchString, string courseSubject, string courseCategory, string sortCriteria)
        {
            var resultList = courses;

            if (!String.IsNullOrEmpty(searchString))
            {
                resultList = resultList.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList(); ;
            }

            if (!String.IsNullOrEmpty(courseSubject))
            {
                resultList = resultList.Where(x => x.Subject.ToLower().Contains(courseSubject.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(sortCriteria))
            {
                var sortCriteriaQuery = Enum.GetNames(typeof(CourseSortCriteria));
                CourseSortCriteria cat;
                Enum.TryParse(sortCriteria, out cat);
                switch (cat)
                {
                    case CourseSortCriteria.Price:
                        resultList = courses.OrderBy(x => x.Price).ToList();
                        break;

                    case CourseSortCriteria.ReleaseDate:
                        resultList = courses.OrderBy(x => x.ReleaseDate).ToList();
                        break;
                }
            }

            if (!String.IsNullOrEmpty(courseCategory))
            {
               
                CourseCategory cat;
                Enum.TryParse(courseCategory, out cat);
                resultList = courses.Where(x => x.Category == cat).ToList();
            }

            return resultList;

        }

        public List<Course> FilterCourseList(ApplicationDbContext context, string searchString, string courseSubject, string courseCategory, string sortCriteria)
        {
            var courseRepository = new CourseRepository(context);
            var resultList = courseRepository.GetCourseListWithTeacherInfo().Where(x => x.IsActive).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                resultList = resultList.Where(x => x.Title.ToLower().Contains(searchString.ToLower())).ToList(); ;
            }

            if (!String.IsNullOrEmpty(courseSubject))
            {
                resultList = resultList.Where(x => x.Subject.ToLower().Contains(courseSubject.ToLower())).ToList();
            }

            if (!String.IsNullOrEmpty(sortCriteria))
            {
                var sortCriteriaQuery = Enum.GetNames(typeof(CourseSortCriteria));
                CourseSortCriteria cat;
                Enum.TryParse(sortCriteria, out cat);
                switch (cat)
                {
                    case CourseSortCriteria.Price:
                        resultList = resultList.OrderBy(x => x.Price).ToList();
                        break;

                    case CourseSortCriteria.ReleaseDate:
                        resultList = resultList.OrderBy(x => x.ReleaseDate).ToList();
                        break;
                }
            }

            if (!String.IsNullOrEmpty(courseCategory))
            {

                CourseCategory cat;
                Enum.TryParse(courseCategory, out cat);
                resultList = resultList.Where(x => x.Category == cat).ToList();
            }

            return resultList;

        }
    }
}
