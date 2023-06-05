using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDB.Models;
using ProjectDB.Services;

namespace ProjectDB.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IManageStudentsService Service;
        private readonly IManageDerpartmentService derpartmentService;
        public StudentsController(IManageStudentsService service,
            IManageDerpartmentService derpartmentService)

        {
            this.Service = service;
            this.derpartmentService = derpartmentService;

        }

        // GET: StudentsController
        public ActionResult Index()
        {
            return View(Service.GetAllStudents());
        }


        // GET: StudentsController/Create
        public ActionResult Create()
        {
            ViewData["departments"] = derpartmentService.GetAllDepartments();
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string dept_name , Student student)
        {
            try
            {
                if (student != null && !string.IsNullOrEmpty(dept_name))
                {
                    student.department = derpartmentService.GetDepartmentsByName(dept_name);
                    Service.AddStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Create));
            }
            catch
            {
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int std_id)
        {
            return View(Service.GetStudent(std_id));
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int std_id, Student student)
        {
            try
            {
                if(student != null && std_id == student.std_id)
                {
                    Service.UpdateStudent(student); 
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int std_id)
        {
            return View(Service.GetStudent(std_id));
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int std_id, Student student)
        {
            try
            {
                if(student != null && student.std_id == std_id)
                {
                    Service.DeleteStudent(std_id);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Delete));
            }
            catch
            {
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}
