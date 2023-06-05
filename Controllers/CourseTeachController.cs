using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDB.Models;
using ProjectDB.Services;
using ProjectDB.Utilites;

namespace ProjectDB.Controllers
{
    [Authentication(role: "admin")]
    public class CourseTeachController : Controller
    {
        readonly IManageCourseTeachService service;
        readonly IManageCourseService courseService;
        readonly IManageInstructorService instructorService;

        public CourseTeachController (
            IManageCourseTeachService service, 
            IManageCourseService courseService,
            IManageInstructorService instructorService)
        {
            this.service = service;
            this.courseService = courseService;
            this.instructorService = instructorService;
        }


        // GET: CourseTeachController
        public ActionResult Index(string searchString)
        {
            List<CourseTeach> courseTeachs = service.GetAllCourseTeachs().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                courseTeachs = courseTeachs.Where(ct => ct.course.title.Contains(searchString)).ToList();
            }

             ViewData["SearchString"] = searchString;

            return View(courseTeachs);
        }



        // GET: CourseTeachController/Create
        public ActionResult Create()
        {
            List<Course>? courses = courseService.GetAllCourses();
            if (courses != null)
            {
                SelectList selects = new SelectList(courses, "course_Id", "title");
                ViewData["courses"] = selects;
            }
            else
                return RedirectToAction("Create", "Courses");
            List<Instructor>? instructors = instructorService.GetAllInstructors();
            if (instructors != null)
            {
                SelectList selects  = new SelectList(instructors,"inst_id","name");
                ViewData["instructors"] = selects;
            }
            else
                return RedirectToAction("Create", "Instructors");
            return View();
        }

        // POST: CourseTeachController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int course_id , int inst_id ,CourseTeach courseTeach)
        {
            try
            {
                var course = courseService.GetCourseById(course_id);
                var instructor = instructorService.GetInstructorById(inst_id);
                if(courseTeach != null && course != null & instructor != null)
                {
                    courseTeach.course = course;
                    courseTeach.instructor = instructor;
                    service.AddCourseTeach(courseTeach);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return RedirectToAction("Create");
            }
        }

        // GET: CourseTeachController/Edit/5
        public ActionResult Edit(int course_id , int inst_id)
        {
            return View(service.GetCourseTeach(course_id,inst_id));
        }

        // POST: CourseTeachController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int course_id,int inst_id, CourseTeach courseTeach)
        {
            try
            {
                var course = courseService.GetCourseById(course_id);
                var instructor = instructorService.GetInstructorById (inst_id);
                if(courseTeach != null && course != null && instructor != null)
                {
                    courseTeach.course = course;
                    courseTeach.instructor = instructor;
                    service.UpdateCourseTeach(courseTeach);
                    return RedirectToAction(nameof(Index));
                }
                return View(service.GetCourseTeach(course_id, inst_id));
            }
            catch
            {
                return View(service.GetCourseTeach(course_id, inst_id));
            }
        }

        // GET: CourseTeachController/Delete/5
        public ActionResult Delete(int course_id, int inst_id)
        {
            return View(service.GetCourseTeach(course_id , inst_id));
        }

        // POST: CourseTeachController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int course_id ,int inst_id, CourseTeach courseTeach)
        {
            try
            {
                var course = courseService.GetCourseById(course_id);
                var instructor = instructorService.GetInstructorById(inst_id);
                if (courseTeach != null && course != null && instructor != null)
                {
                    service.DeleteCourseTeach(course_id, inst_id);
                    return RedirectToAction(nameof(Index));
                }
                return View(service.GetCourseTeach(course_id, inst_id));
            }
            catch
            {
                return View(service.GetCourseTeach(course_id, inst_id));
            }
        }
    }
}
