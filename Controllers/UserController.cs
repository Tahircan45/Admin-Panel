using Admin_Panel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin_Panel.Controllers
{
    [Authorize(Roles="Manager")]
    public class UserController : Controller
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<WebUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        [HttpPost]
        public IActionResult UserList(string? keyword)
        {
            var roleresult = _userManager.Users.Where(user =>
                                         user.UserName.Contains(keyword) ||
                                         user.Email.Contains(keyword) ||
                                         user.FirstName.Contains(keyword))
                                         .ToList();
            return View(roleresult);
        }
    }
}
