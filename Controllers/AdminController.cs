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
                if (Role.Equals("Barber"))
                {
                    // Create new Barber page
                    //InstructorRelation instructor = _context.InstructorRelation.Where(o => o.Username == Username).FirstOrDefault();

                    // add to customer
                    //if (instructor == null) // create Instructor course.
                    //{
                    //    _context.InstructorRelation.Add(new InstructorRelation() { Username = Username });
                    //    _context.SaveChanges();
                    //}
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