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

        [HttpGet("{id}")]
        public async Task<ActionResult<WebUser>> UserEdit(string id)
        {
            ViewData["roles"] = _roleManager.Roles.ToList();            
            var user= await _userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult<WebUser>> AddRole(string rolename,string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var  result= await _userManager.AddToRoleAsync(user,rolename);
            return RedirectToAction("UserList");
        }
        [HttpPost]
        public async Task<ActionResult<WebUser>> DeleteRole(string rolename, string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.RemoveFromRoleAsync(user, rolename);
            return RedirectToAction("UserList");
        }
    }
}
