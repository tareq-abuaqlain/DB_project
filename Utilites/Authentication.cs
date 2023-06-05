using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectDB.Models;
using ProjectDB.Services;

namespace ProjectDB.Utilites
{
    public class Authentication : ActionFilterAttribute
    {
        string role = "";
        private List<string> roles = new List<string>();
        readonly IManageUserServices userServices;
        readonly IManageRolesService rolesService;
        /*
         Authentication(role:"role1,...,rolex")
         */
        public Authentication()
            : this(new ManageUserServices(), new ManageRolesService(), "")
        {

        }
        /// <summary>
        /// Authentication(role:"role1,...,rolex")
        /// </summary>
        /// <param name=""></param>

        public Authentication(string role)
            :this(new ManageUserServices(), new ManageRolesService(), role)
        {
            
        }
        public Authentication(
            IManageUserServices userServices,
            IManageRolesService rolesService,
            string role = "") {
            this.role = role.Replace(" ","");
            roles = role.Split(',').ToList();
            
            this.userServices = userServices;
            this.rolesService = rolesService;
        } 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {
                var user = userServices.GetUserByUserName(username!);
                if (user != null && roles != null && !string.IsNullOrEmpty(role) && roles.Count() != 0)
                {
                    var role_user = rolesService.GetAllRolesByUser_id(user.user_id);
                    if (role_user != null)
                    {
                        var role_userList = role_user.Select(role => role.name).ToList();
                        if (!roles.Any(item => role_userList.Contains(item)))
                        {
                            context.Result = new RedirectToRouteResult(
                                new RouteValueDictionary
                                {
                                    {"Controller","Home" },
                                    {"Action","Privacy" }
                                }
                                );
                        }
                    }
                    else
                    {
                        context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary
                            {
                        {"Controller","Home" },
                        {"Action","Privacy" }
                            }
                            );
                    }
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                    {"Controller","Home" },
                    {"Action","Privacy" }
                    }
                    );
            }
        }
    }
}
