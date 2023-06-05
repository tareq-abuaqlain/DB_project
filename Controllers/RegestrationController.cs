using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDB.Models;
using ProjectDB.Services;

namespace ProjectDB.Controllers
{
    public class RegestrationController : Controller
    {
        private readonly IManageRegestrationService service;
        private readonly IManageStudentsService studentsService;
        private readonly IManageDivService divService;
        private readonly IManageCourseService courseService;

        public RegestrationController(IManageRegestrationService service,
            IManageStudentsService studentsService,
            IManageDivService divService,
            IManageCourseService courseService)
        {
            this.service = service;
           this.studentsService = studentsService;
            this.divService = divService;
            this.courseService = courseService;
        }
        // GET: RegestrationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegestrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegestrationController/Create
        public ActionResult Create(int course_id , int div_id)
        {
            var reg = new Registration();
            reg.div = divService.GetDivById(div_id);
            reg.course = courseService.GetCourseById(course_id);
            return View(reg);
        }

        // POST: RegestrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int std_id,int course_id , int div_id)
        {
            try
            {
                var student = studentsService.GetStudent(std_id);
                if(student != null)
                {
                    if (!service.RegisterStudent(std_id, course_id, div_id))
                        return RedirectToAction("IndexStudentByDiv", "Divs", new { div_id = div_id });
                    else
                        return RedirectToAction(nameof(Create), new { course_id = course_id , div_id = div_id });
                }
                return RedirectToAction("Create","Students");
                
            }
            catch
            {
                return RedirectToAction(nameof(Create), new { course_id = course_id, div_id = div_id });
            }
        }

        // GET: RegestrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegestrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegestrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegestrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
