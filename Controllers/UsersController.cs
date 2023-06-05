using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDB.Models;
using ProjectDB.Services;
using ProjectDB.Utilites;

namespace ProjectDB.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IManageUserServices services;
        readonly ILogger<InputModel> logger;
        readonly IManageRolesService rolesService;
        readonly IManageUserRoleService roleUserService;

        public UsersController(IManageUserServices services,
            ILogger<InputModel> logger, 
            IManageRolesService rolesService,
            IManageUserRoleService roleUserService)
        {
            this.services = services;
            this.logger = logger;
            this.rolesService = rolesService;
            this.roleUserService = roleUserService;
        }

        //Get
        [HttpGet]
        public ActionResult Login()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        //post
        [HttpPost]
        public ActionResult Login(InputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = services.GetUserByUserName(inputModel.username??"");

                if (user != null)
                {
                    var result = false;
                    if (!string.IsNullOrEmpty(inputModel.username) && !string.IsNullOrEmpty(inputModel.Password))
                     result = signIn(user.username??"", inputModel.Password??"");

                    if (result)
                    {
                        logger.LogInformation("تم تسجيل الدخول بنجاح");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "فشل عملية تسجيل الدخول");
                        return RedirectToAction(nameof(Login));
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "فشل عملية تسجيل الدخول");
                }
            }
            // If we got this far, something failed, redisplay form
            return RedirectToAction(nameof(Login));
        }

        private bool signIn(string username, string password)
        {
            var user = services.GetUserByUserNameAndPassword(username, password);
            if (user != null)
            {
                var roles = rolesService.GetAllRolesByUser_id(user.user_id);
                int i = 0;
                foreach (var role in roles!)
                {
                    if (i == 0)
                        HttpContext.Session.SetString("role", role.name??"");
                    else
                        HttpContext.Session.SetString("role_" + i, role.name ?? "");
                    
                    i++;
                }
                HttpContext.Session.SetString("username", user.username ?? "");
                return true;
            }
            return false;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            return RedirectToAction("Login");
        }


        [Authentication(role:"admin")]
public ActionResult Index(string searchString)
{
    ViewData["userRoles"] = roleUserService.GetAllUserRole();
    ViewData["roles"] = rolesService.GetAllRoles();

    List<User> users = services.GetAllUsers().ToList(); // Convert IEnumerable to List

    if (!string.IsNullOrEmpty(searchString))
    {
        users = users.Where(u => u.username.Contains(searchString)).ToList();
    }

    return View(users);
}

        [Authentication(role: "admin")]
        // GET: UsersController/Create
        public ActionResult Create()
        {
            ViewData["roles"] = rolesService.GetAllRoles();
            return View();
        }
        [Authentication(role: "admin")]
        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int role_id, User user)
        {
            try
            {
                if(role_id !=0 && user != null)
                {
                    var result = services.AddUser(user);
                    if (result)
                    {
                        var userAdded = services.GetUserByUserName(user.username??"");
                        if (rolesService.RoleIsExit(role_id))
                        {
                            if(roleUserService.AddUserRole(userAdded!.user_id, role_id))
                                return RedirectToAction(nameof(Index));
                        }
                        
                    }
                    
                }
                ViewData["roles"] = rolesService.GetAllRoles();
                return View();

            }
            catch
            {
                ViewData["roles"] = rolesService.GetAllRoles();
                return View();
            }
        }
        [Authentication(role: "admin")]
        // GET: UsersController/Edit/5
        public ActionResult Edit(int user_id)
        {
            
            return View(services.GetUserById(user_id));
        }
        [Authentication(role: "admin")]
        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int user_id, User user)
        {
            try
            {
                if(user != null && user_id == user.user_id)
                {
                    services.UpdateUser(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(services.GetUserById(user_id));
            }
            catch
            {
                return View(services.GetUserById(user_id));
            }
        }
        [Authentication(role: "admin")]
        // GET: UsersController/Delete/5
        public ActionResult Delete(int user_id)
        {
            return View(services.GetUserById(user_id));
        }
        [Authentication(role: "admin")]
        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int user_id, User user)
        {
            try
            {
                if (user_id == 1)
                    return RedirectToAction("Privacy", "Home");
                if(user!=null && user_id == user.user_id )
                {
                    var username = HttpContext.Session.GetString("username");
                    if (string.IsNullOrEmpty(username))
                    {
                        return RedirectToAction(nameof(Logout));
                    }
                    roleUserService.DeleteAllUserRole(user_id);
                    services.DeleteUser(user_id);
                    if (username.Equals(user.username))
                    {
                        return RedirectToAction(nameof(Logout));
                    }
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(services.GetUserById(user_id));
            }
        }
    }
}
