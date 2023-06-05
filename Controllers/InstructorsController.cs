using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDB.Models;
using ProjectDB.Services;
using ProjectDB.Utilites;

namespace ProjectDB.Controllers
{
    [Authentication(role: "admin")]
    public class InstructorsController : Controller
    {
        private readonly IManageInstructorService service;
        private readonly IManageDerpartmentService dept_service;

        public InstructorsController(IManageInstructorService service, IManageDerpartmentService dept_service)
        {
            this.service = service;
            this.dept_service = dept_service;
        }

    // GET: InstructorsController
public ActionResult Index(string searchString)
{
    ViewBag.CurrentFilter = searchString;

    var instructors = service.GetAllInstructors();

    if (!string.IsNullOrEmpty(searchString))
    {
        instructors = instructors.Where(i =>
            i.name != null && i.name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
            i.department != null && i.department.dept_name != null &&
            i.department.dept_name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    return View(instructors);
}




    // GET: InstructorsController/Create
    public ActionResult Create(string dept_name)
    {
        List<Department> departments = dept_service.GetAllDepartments()?.ToList();
        if (departments == null || departments.Count <= 0)
            return RedirectToAction("Create", "Department");
        SelectList selectList = new SelectList(departments, "dept_name", "dept_name");
        ViewData["List"] = selectList;
        return View();
    }



        // POST: InstructorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string dept_name, Instructor instructor)
        {
            try
            {
                if (instructor != null)
                {
                    if (instructor.salary <= 29000)
                    {
                        ModelState.AddModelError("salary", "Salary cannot be below 29000.");
                        List<Department>? deptList = dept_service.GetAllDepartments();
                        if (deptList == null || deptList.Count() <= 0)
                            return RedirectToAction("Create", "Department");
                        SelectList selectList = new SelectList(deptList, "dept_name", "dept_name");
                        ViewData["List"] = selectList;
                        return View(instructor);
                    }

                    instructor.department = dept_service.GetDepartmentsByName(dept_name);
                    service.AddInstructor(instructor);
                    return RedirectToAction(nameof(Index));
                }

                List<Department>? deptList2 = dept_service.GetAllDepartments();
                if (deptList2 == null || deptList2.Count() <= 0)
                    return RedirectToAction("Create", "Department");
                SelectList selectList2 = new SelectList(deptList2, "dept_name", "dept_name");
                ViewData["List"] = selectList2;
                return View();
            }
            catch
            {
                List<Department>? deptList3 = dept_service.GetAllDepartments();
                if (deptList3 == null || deptList3.Count() <= 0)
                    return RedirectToAction("Create", "Department");
                SelectList selectList3 = new SelectList(deptList3, "dept_name", "dept_name");
                ViewData["List"] = selectList3;
                return View();
            }
        }

        // GET: InstructorsController/Edit/5
        public ActionResult Edit(int inst_id)
        {
            List<Department>? deptList = dept_service.GetAllDepartments();
            if (deptList == null || deptList.Count() <= 0)
                return RedirectToAction("Create", "Department");
            SelectList selectList = new SelectList(deptList, "dept_name", "dept_name");
            ViewData["List"] = selectList;
            return View(service.GetInstructorById(inst_id));
        }

        // POST: InstructorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int inst_id, string dept_name, Instructor instructor)
        {
            try
            {
                if (instructor != null && inst_id == instructor.inst_id)
                {
                    instructor.department = dept_service.GetDepartmentsByName(dept_name);
                    service.UpdateInstructor(instructor);
                    return RedirectToAction(nameof(Index));
                }
                List<Department>? deptList = dept_service.GetAllDepartments();
                if (deptList == null || deptList.Count() <= 0)
                    return RedirectToAction("Create", "Department");
                SelectList selectList = new SelectList(deptList, "dept_name", "dept_name");
                ViewData["List"] = selectList;
                return View(service.GetInstructorById(inst_id));
            }
            catch
            {
                List<Department>? deptList = dept_service.GetAllDepartments();
                if (deptList == null || deptList.Count() <= 0)
                    return RedirectToAction("Create", "Department");
                SelectList selectList = new SelectList(deptList, "dept_name", "dept_name");
                ViewData["List"] = selectList;
                return View(service.GetInstructorById(inst_id));
            }
        }
        // GET: InstructorsController/Delete/5

        public ActionResult Delete(int inst_id)
        {
            return View(service.GetInstructorById(inst_id));
        }

        // POST: InstructorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int inst_id, Instructor instructor)
        {
            try
            {
                if (instructor != null && inst_id == instructor.inst_id)
                {
                    service.DeleteInstructor(inst_id);
                    return RedirectToAction(nameof(Index));
                }
                return View(service.GetInstructorById(inst_id));
            }
            catch
            {
                return View(service.GetInstructorById(inst_id));
            }
        }
    }
}
