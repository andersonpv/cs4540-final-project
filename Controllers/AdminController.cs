/*
    CS 4540 Web Software Architecture
    Admin Controller
    Author: Kevin Nguyen
    Date: 10-18-2019
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs4540_final_project.Data;
using cs4540_final_project.Models;
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
        private readonly WorkerContext _context;

        public AdminController(WorkerContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("/Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            return View(await _userManager.Users.OrderBy(o => o.NormalizedUserName).ToListAsync());
        }


        [HttpPost]
        [Route("/Admin/ChangeRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string Username, string Role, string AddRemove)
        {
            IdentityUser user = await _userManager.FindByNameAsync(Username);
            IdentityRole UsrRole = await _roleManager.FindByNameAsync(Role);

            if (AddRemove.Equals("Add"))
            {
                if (Role.Equals("Worker"))
                {
                    // Create new Worker
                    
                    Worker worker = new Worker()
                    {
                        Name = Username,
                        User = user,
                    };
                    _context.Add(worker);
                    _context.SaveChanges();
                }

                await _userManager.AddToRoleAsync(user, UsrRole.Name);
                return Json(new { success = true, errorMessage = "" });
            }
            else if (AddRemove.Equals("Remove"))
            {
                if (Role.Equals("Admin"))
                {
                    IList<IdentityUser> AdminUsers = await _userManager.GetUsersInRoleAsync("Admin");
                    if (AdminUsers.Count <= 1)
                    {
                        return Json(new { success = false, errorMessage = "Can't Remove Last Admin from Database." });
                    }
                }
                await _userManager.RemoveFromRoleAsync(user, UsrRole.Name);

                return Json(new { success = true, errorMessage = "" });
            }
            return Json(new { success = false, errorMessage = "" });
        }
    }
}