using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDB.Models;
using ProjectDB.Services;
using ProjectDB.Utilites;

namespace ProjectDB.Controllers
{
    [Authentication(role: "admin")]
    public class CoursesController : Controller
    {
        private readonly IManageCourseService service;
        private readonly IManageDerpartmentService derpartmentService;

        public CoursesController(IManageCourseService service,
            IManageDerpartmentService derpartmentService)
        {
            this.service = service;
            this.derpartmentService = derpartmentService;
        }


        // GET: CoursesController
        public ActionResult Index(string searchString)
        {
            List<Course> courses = service.GetAllCourses().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.department.dept_name.Contains(searchString)).ToList();
            }

            return View(courses);
        }


        // GET: CoursesController/Create
        public ActionResult Create()
        {
            ViewData["departments"] = derpartmentService.GetAllDepartments(); 
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string dept_name , Course course)
        {
            try
            {
                if(!string.IsNullOrEmpty(dept_name) && course != null)
                {
                    course.department = derpartmentService.GetDepartmentsByName(dept_name);
                    service.AddCourse(course);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["departments"] = derpartmentService.GetAllDepartments();
                return View();
            }
            catch
            {
                ViewData["departments"] = derpartmentService.GetAllDepartments();
                return View();
            }
        }

        // GET: CoursesController/Edit/5
        public ActionResult Edit(int course_id)
        {
            ViewData["departments"] = derpartmentService.GetAllDepartments();
            return View(service.GetCourseById(course_id));
        }

        // POST: CoursesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int course_id,string dept_name, Course course)
        {
            try
            {
                if(!string.IsNullOrEmpty(dept_name) && course != null && course.course_Id == course_id)
                {
                    course.department = derpartmentService.GetDepartmentsByName(dept_name);
                    service.UpdateCourse(course);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["departments"] = derpartmentService.GetAllDepartments();
                return View(service.GetCourseById(course_id));
            }
            catch
            {
                ViewData["departments"] = derpartmentService.GetAllDepartments();
                return View(service.GetCourseById(course_id));
            }
        }

        // GET: CoursesController/Delete/5
        public ActionResult Delete(int course_id)
        {
            return View(service.GetCourseById(course_id));
        }

        // POST: CoursesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int course_id, Course course)
        {
            try
            {
                if (course != null && course_id == course.course_Id){
                    service.DeleteCourse(course_id);
                    return RedirectToAction(nameof(Index));
                }
                return View(service.GetCourseById(course_id));
            }
            catch
            {
                return View(service.GetCourseById(course_id));
            }
        }
    }
}
