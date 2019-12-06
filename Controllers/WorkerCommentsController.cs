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

        // GET: WorkerComments
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewData["workers"] = _context.Worker.ToList();
            return View(await _context.WorkerComment.OrderBy(m => m.Worker.Name).ToListAsync());
        }

        // GET: WorkerComments
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

            //ViewData["workers"] = _context.Worker.ToList();
            //return View(await _context.WorkerComment.OrderBy(m => m.Worker.Name).ToListAsync());
        }

        // GET: WorkerComments/Details/5
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


        // GET: WorkerComments/Create
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> ClientCreate(int? id)
        {
            ViewDataSelectOneWorker((int)id);
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Username"] = user.UserName;

            return View();
        }

        // POST: WorkerComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: WorkerComments/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewDataSelectWorkers();
            return View();
        }

        // POST: WorkerComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: WorkerComments/Edit/5
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

        // POST: WorkerComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                if ( !User.IsInRole("Admin") && (!user.UserName.ToUpper().Equals(workerComment.Name.ToUpper())))
                    return Forbid();

                try
                {
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
                return RedirectToAction("ShowComments", new { id = workerComment.WorkerID });
            }
            return View(workerComment);
        }

        // GET: WorkerComments/Delete/5
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

            return RedirectToAction("ShowComments", new { id = workerComment.WorkerID });
        }

        // POST: WorkerComments/Delete/5
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
            return RedirectToAction("ShowComments", new { id = workerComment.WorkerID });
        }

        private bool WorkerCommentExists(int id)
        {
            return _context.WorkerComment.Any(e => e.WorkerCommentID == id);
        }
    }
}
