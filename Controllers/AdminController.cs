/*
    CS 4540 Web Software Architecture
    Admin Controller
    Author: Kevin Nguyen
    Date: 10-18-2019
*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS4540_LOT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("/Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            
            return View(await _userManager.Users.ToListAsync());
        }


        [HttpPost]
        [Route("/Admin/ChangeRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string Username, string Role, string AddRemove)
        {
            IdentityUser user = await _userManager.FindByNameAsync(Username);
            IdentityRole UsrRole = await _roleManager.FindByNameAsync(Role);
            return null;
        }
    }
}