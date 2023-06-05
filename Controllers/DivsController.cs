using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDB.Models;
using ProjectDB.Services;
using ProjectDB.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectDB.Controllers
{
    [Authentication]
    public class DivsController : Controller
    {
        private readonly IManageDivService service;
        private readonly IManageCourseService courseService;
        private readonly IManageInstructorService instructorService;
        private readonly IManageUserServices userService;
        private readonly IManageRegestrationService regestrationService;

        public DivsController(IManageDivService service,
            IManageCourseService courseService,
            IManageInstructorService instructorService,
            IManageUserServices userService,
            IManageRegestrationService regestrationService)
        {
            this.service = service;
            this.courseService = courseService;
            this.instructorService = instructorService;
            this.userService = userService;
            this.regestrationService = regestrationService;
        }

        [Authentication(role: "admin,assistant")]
        // GET: Divs By User
        public ActionResult IndexStudentByDiv(int div_id)
        {
            var div = service.GetDivById(div_id);
            if (div != null)
            {
                var usernameLogin = HttpContext.Session.GetString("username");
                if (!string.IsNullOrEmpty(usernameLogin) && !string.IsNullOrEmpty(div.user!.username))
                {
                    return View(regestrationService.GetAllStudentsInDiv(div_id));
                }
            }
            return RedirectToAction("Privacy", "Home");
        }

        [Authentication(role: "admin,assistant")]
        // GET: Divs By User
        public ActionResult IndexByUser(string username)
        {
            var user = userService.GetUserByUserName(username);
            if (user != null)
            {
                var usernameLogin = HttpContext.Session.GetString("username");
                if (!string.IsNullOrEmpty(usernameLogin) && !string.IsNullOrEmpty(user.username))
                {
                    return View(service.GetAllDivsByUsername(username));
                }
            }
            return RedirectToAction("Privacy", "Home");
        }

        [Authentication(role: "admin")]
        // GET: DivsController
        public ActionResult Index(string searchString)
        {
            List<Div> divs = service.GetAllDivs().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                divs = divs.Where(d => d.div_id.ToString().Contains(searchString)).ToList();
            }

            ViewBag.CurrentFilter = searchString;

            return View(divs);
        }

        [Authentication(role: "admin")]
        // GET: DivsController/Create
        public ActionResult Create(int course_id, int inst_id)
        {
            ViewData["course"] = courseService.GetCourseById(course_id);
            ViewData["instructor"] = instructorService.GetInstructorById(inst_id);
            var users = userService.GetAllUsersByRole("assistant");
            if (users != null)
                ViewData["users"] = new SelectList(users, "user_id", "name", users.FirstOrDefault());
            else
                return RedirectToAction("Create", "Users");

            var types = Enum.GetValues(typeof(typePerson)).Cast<typePerson>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            ViewData["types"] = new SelectList(types, "Value", "Text");
            return View();
        }

        [Authentication(role: "admin")]
        // POST: DivsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int course_id, int inst_id, int user_id, Div div)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    div.course = courseService.GetCourseById(course_id);
                    div.instructor = instructorService.GetInstructorById(inst_id);
                    div.user = userService.GetUserById(user_id);
                    service.AddDiv(div);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to create a new div. Please try again.");
            }

            ViewData["course"] = courseService.GetCourseById(course_id);
            ViewData["instructor"] = instructorService.GetInstructorById(inst_id);
            var users = userService.GetAllUsersByRole("assistant");
            if (users != null)
                ViewData["users"] = new SelectList(users, "user_id", "name", users.FirstOrDefault());

            var types = Enum.GetValues(typeof(typePerson)).Cast<typePerson>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            ViewData["types"] = new SelectList(types, "Value", "Text");

            return View(div);
        }

        [Authentication(role: "admin")]
        // GET: DivsController/Edit/5
        public ActionResult Edit(int id)
        {
            var div = service.GetDivById(id);
            if (div != null)
            {
                var users = userService.GetAllUsersByRole("assistant");
                if (users != null)
                    ViewData["users"] = new SelectList(users, "user_id", "name", div.user?.user_id);

                var types = Enum.GetValues(typeof(typePerson)).Cast<typePerson>().Select(
                    v => new SelectListItem
                    {
                        Text = v.ToString(),
                        Value = ((int)v).ToString()
                    }).ToList();
                ViewData["types"] = new SelectList(types, "Value", "Text", div.type);

                return View(div);
            }
            else
            {
                return NotFound();
            }
        }

        [Authentication(role: "admin")]
        // POST: DivsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Div div)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.UpdateDiv(div);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to update the div. Please try again.");
            }

            var users = userService.GetAllUsersByRole("assistant");
            if (users != null)
                ViewData["users"] = new SelectList(users, "user_id", "name", div.user?.user_id);

            var types = Enum.GetValues(typeof(typePerson)).Cast<typePerson>().Select(
                v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            ViewData["types"] = new SelectList(types, "Value", "Text", div.type);

            return View(div);
        }

        [Authentication(role: "admin")]
        // GET: DivsController/Delete/5
        public ActionResult Delete(int id)
        {
            var div = service.GetDivById(id);
            if (div != null)
            {
                return View(div);
            }
            else
            {
                return NotFound();
            }
        }

        [Authentication(role: "admin")]
        // POST: DivsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                service.DeleteDiv(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to delete the div. Please try again.");
            }

            var div = service.GetDivById(id);
            if (div != null)
            {
                return View(div);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
