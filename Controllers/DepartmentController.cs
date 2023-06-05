using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using ProjectDB.Models;
using ProjectDB.Services;
using ProjectDB.Utilites;

namespace ProjectDB.Controllers
{
    [Authentication(role: "admin")]
    public class DepartmentController : Controller
    {
        readonly IManageDerpartmentService service;
        public DepartmentController(IManageDerpartmentService service)
        {
            this.service = service;
        }


        // GET: DepartmentController
        public ActionResult Index(string search)
        {
            var departments = service.GetAllDepartments();

            if (!string.IsNullOrEmpty(search))
            {
                departments = departments.Where(d => d.dept_name.Contains(search)).ToList();
            }

            return View(departments);
        }


        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            try
            {
                if(department != null)
                { 
                    service.AddDepartment(department);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(string dept_name)
        {
            
            return View(service.GetDepartmentsByName(dept_name));
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string dept_name, Department department)
        {
            try
            {

                if (department != null && dept_name == department.dept_name)
                {
                    service.UpdateDepartment(department);
                    return RedirectToAction(nameof(Index));
                }
                return View(department);
            }
            catch
            {
                return View(department);
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(string dept_name)
        {
            return View(service.GetDepartmentsByName(dept_name));
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string dept_name, Department department)
        {
            try
            {
                if (dept_name == department.dept_name)
                {
                    service.DeleteDepartment(dept_name);
                    return RedirectToAction(nameof(Index));
                }
                return View(service.GetDepartmentsByName(dept_name));
            }
            catch
            {
                return View(service.GetDepartmentsByName(dept_name));
            }
        }
    }
}
