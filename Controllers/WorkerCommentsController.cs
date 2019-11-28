using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cs4540_final_project.Data;
using cs4540_final_project.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkerComment.ToListAsync());
        }

        // GET: WorkerComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerComment = await _context.WorkerComment
                .FirstOrDefaultAsync(m => m.WorkerCommentID == id);
            if (workerComment == null)
            {
                return NotFound();
            }

            return View(workerComment);
        }

        // GET: WorkerComments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkerComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerCommentID,Comment,StarRating,LastUpdated")] WorkerComment workerComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workerComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workerComment);
        }

        // GET: WorkerComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("WorkerCommentID,Comment,StarRating,LastUpdated")] WorkerComment workerComment)
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerComment = await _context.WorkerComment
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
