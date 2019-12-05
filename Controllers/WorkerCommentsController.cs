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

namespace cs4540_final_project.Controllers
{
    public class WorkerCommentsController : Controller
    {
        private readonly WorkerContext _context;

        public WorkerCommentsController(WorkerContext context)
        {
            _context = context;
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
        [Authorize(Roles = "Admin")]
        public IActionResult ClientCreate()
        {
            ViewDataSelectWorkers();
            return View();
        }

        // POST: WorkerComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientCreate([Bind("WorkerCommentID,Name,Comment,StarRating,LastUpdated,WorkerID")] WorkerComment workerComment)
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

        private void ViewDataSelectWorkers()
        {
            var instructors =
               (from Instructors in _context.Worker
                select Instructors.Name).FirstOrDefault();

            IEnumerable items = _context.Worker.Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = "" + c.ID
                }).ToList();

            ViewData["WorkerSelect"] = items;
        }

        // GET: WorkerComments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            ViewDataSelectWorkers();

            var workerComment = await _context.WorkerComment.FindAsync(id);

            if (workerComment == null)
            {
                return NotFound();
            }
            return View(workerComment);
        }

        // POST: WorkerComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerCommentID,Name,Comment,StarRating,LastUpdated,WorkerID")] WorkerComment workerComment)
        {


            if (id != workerComment.WorkerCommentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            return View(workerComment);
        }

        // GET: WorkerComments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
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

        // POST: WorkerComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workerComment = await _context.WorkerComment.FindAsync(id);
            _context.WorkerComment.Remove(workerComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerCommentExists(int id)
        {
            return _context.WorkerComment.Any(e => e.WorkerCommentID == id);
        }
    }
}
