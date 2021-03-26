using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin_Panel.Controllers
{
    [Authorize(Roles ="Manager")]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult RoleList()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult RoleCreate()
        {
            return View(new IdentityRole());
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("RoleList");
        }
    }
}
