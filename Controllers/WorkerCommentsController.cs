/**
 * CS 4540 Web Software Architecture
 * WorkerCommentsController
 * Authors: Kevin Nguyen
 * Date: 12-6-2019
 * 
 * Controller for Worker Comments
 **/

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cs4540_final_project.Data;
using cs4540_final_project.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Identity;

namespace cs4540_final_project.Controllers
{
    public class WorkerCommentsController : Controller
    {
        private readonly WorkerContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WorkerCommentsController(WorkerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        /// <summary>
        /// Shows a list of all worker comments 
        /// GET: WorkerComments 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewData["workers"] = _context.Worker.ToList();
            return View(await _context.WorkerComment.OrderBy(m => m.Worker.Name).ToListAsync());
        }
        

        /// <summary>
        /// Displays the comments of a specific worker
        /// GET: WorkersComments/ShowComments/5
        /// </summary>
        /// <param name="id">Worker's id</param>
        /// <returns></returns>
        public async Task<IActionResult> ShowComments(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["workers"] = _context.Worker.ToList();
            List<WorkerComment> workerComment = _context.WorkerComment
                .Include(m => m.Worker)
                .Where(m => m.WorkerID == id)
                .ToList();

            if (workerComment == null)
            {
                return NotFound();
            }

            return View(workerComment);
        }


        /// <summary>
        /// Shows the Details page of a comment
        /// GET: WorkerComments/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerComment = await _context.WorkerComment
                .Include(m => m.Worker)
                .FirstOrDefaultAsync(m => m.WorkerCommentID == id);

            if (workerComment == null)
            {
                return NotFound();
            }

            return View(workerComment);
        }


        /// <summary>
        /// Displays the comment page
        /// GET: WorkerComments/Create 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> ClientCreate(int? id)
        {
            ViewDataSelectOneWorker((int)id);
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Username"] = user.UserName;

            return View();
        }


        /// <summary>
        /// Creates the comment and adds it to the database
        /// POST: WorkerComments/Create
        /// </summary>
        /// <param name="workerComment"></param>
        /// <returns>View to redirect user</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> ClientCreate([Bind("WorkerCommentID,Name,Comment,StarRating,LastUpdated,WorkerID")] WorkerComment workerComment)
        {
            if (ModelState.IsValid)
            {
                Worker worker = _context.Worker.Where(m => m.ID == workerComment.WorkerID).FirstOrDefault();
                workerComment.LastUpdated = DateTime.UtcNow.ToLocalTime();
                workerComment.Worker = worker;

                IdentityUser currUser = await _userManager.GetUserAsync(HttpContext.User);

                workerComment.Name = currUser.UserName;

                _context.Add(workerComment);
                await _context.SaveChangesAsync();
                return RedirectToAction("ShowComments", new { id = worker.ID });
            }

            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Username"] = user.UserName;
            ViewDataSelectOneWorker(workerComment.WorkerID);

            return View(workerComment);
        }


        /// <summary>
        /// GET: WorkerComments/Create
        /// </summary>
        /// <returns>View to redirect user</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewDataSelectWorkers();
            return View();
        }


        /// <summary>
        /// POST: WorkerComments/Create
        /// </summary>
        /// <param name="workerComment"></param>
        /// <returns>View to redirect user</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("WorkerCommentID,Name,Comment,StarRating,LastUpdated,WorkerID")] WorkerComment workerComment)
        {
            if (ModelState.IsValid)
            {
                Worker worker = _context.Worker.Where(m => m.ID == workerComment.WorkerID).FirstOrDefault();
                workerComment.Worker = worker;

                _context.Add(workerComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewDataSelectWorkers();
            return View(workerComment);
        }


        /// <summary>
        /// Load ViewData[WorkerSelect] with SelectValues. Gathers all Workers in the Database.
        /// </summary>
        private void ViewDataSelectWorkers()
        {
            IEnumerable items = _context.Worker.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = "" + c.ID
                }).ToList();

            ViewData["WorkerSelect"] = items;
        }


        /// <summary>
        /// Load ViewData[WorkerSelect] with SelectValues. Only loads one worker.
        /// </summary>
        /// <param name="id">The worker's id to be loaded</param>
        private void ViewDataSelectOneWorker(int id)
        {
            IEnumerable items = _context.Worker.Where(m => m.ID == id).Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = "" + c.ID
                }).ToList();
            ViewData["WorkerSelect"] = items;
        }


        /// <summary>
        /// Displays the view for editing comments
        /// GET: WorkerComments/Edit/5 
        /// </summary>
        /// <param name="id">Comment id</param>
        /// <returns>View to redirect user</returns>
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WorkerComment workerComment = await _context.WorkerComment.FindAsync(id);
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (workerComment == null || user == null)
            {
                return NotFound();
            }

            // Must be Admin, or original commenter 
            if (!User.IsInRole("Admin") && (!user.UserName.ToUpper().Equals(workerComment.Name.ToUpper())))
                return Forbid();
            
            ViewDataSelectWorkers();
            return View(workerComment);
        }


        /// <summary>
        /// Edits the comment based on the User's input.
        /// POST: WorkerComments/Edit/5 
        /// </summary>
        /// <param name="id">ID of comment to be edited</param>
        /// <param name="workerComment">the new worker comment to repace the old comment</param>
        /// <returns>View to redirect user</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerCommentID,Name,Comment,StarRating,LastUpdated,WorkerID")] WorkerComment workerComment)
        {
            if (id != workerComment.WorkerCommentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Must be Admin, or original commenter 
                IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (!User.IsInRole("Admin") && (!user.UserName.ToUpper().Equals(workerComment.Name.ToUpper())))
                    return Forbid();

                try
                {
                    workerComment.LastUpdated = DateTime.Now;
                    _context.Update(workerComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerCommentExists(workerComment.WorkerCommentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction(nameof(Index));
                }
                // user is in a Customer role
                return RedirectToAction("ShowComments", new { id = workerComment.WorkerID });
            }
            return View(workerComment);
        }


        /// <summary>
        /// Confirms deletion of a comment. 
        /// GET: WorkerComments/Delete/5
        /// </summary>
        /// <param name="id">The ID of the comment to be deleted.</param>
        /// <returns>View to redirect user</returns>
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            WorkerComment workerComment = await _context.WorkerComment
                        .Include(m => m.Worker)
                        .FirstOrDefaultAsync(m => m.WorkerCommentID == id);

            // Must be Admin, or original commenter 
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (!User.IsInRole("Admin") && (!user.UserName.ToUpper().Equals(workerComment.Name.ToUpper())))
                return Forbid();

            if (workerComment == null)
            {
                return NotFound();
            }
            return View(workerComment);
        }


        /// <summary>
        /// Confirms deletion of a comment. 
        /// POST: WorkerComments/Delete/5
        /// </summary>
        /// <param name="id">The ID of the comment to be deleted.</param>
        /// <returns>View to redirect user</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workerComment = await _context.WorkerComment.FindAsync(id);

            // Must be Admin, or original commenter 
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (!User.IsInRole("Admin") && (!user.UserName.ToUpper().Equals(workerComment.Name.ToUpper())))
                return Forbid();

            _context.WorkerComment.Remove(workerComment);
            await _context.SaveChangesAsync();

            if(User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index));
            }
            // user is in a Customer role
            return RedirectToAction("ShowComments", new { id = workerComment.WorkerID });
        }


        private bool WorkerCommentExists(int id)
        {
            return _context.WorkerComment.Any(e => e.WorkerCommentID == id);
        }
    }
}
