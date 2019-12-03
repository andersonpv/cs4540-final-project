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
    public class WorkersController : Controller
    {
        private readonly WorkerContext _context;

        public WorkersController(WorkerContext context)
        {
            _context = context;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Worker.ToListAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .Include(o => o.Schedule)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // Book an appointment
        public async Task<IActionResult> Book(int? id)
        {
            if (id == null)
                return NotFound();

            var worker = await _context.Worker
                .Include(o => o.Schedule)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }


        public async Task<IActionResult> BookAppointment(int? id, int? scheduleID, string time, string date)
        {
            var worker = await _context.Worker
                .Include(w => w.Schedule)
                .FirstOrDefaultAsync(w => w.ID == id);

            DaySchedule ds = worker.Schedule.FirstOrDefault(o => o.DayScheduleID == scheduleID);

            if (time == "9:00")
                ds.Nine = true;
            else if (time == "9:30")
                ds.NineThirty = true;
            else if (time == "10:00")
                ds.Ten = true;
            else if (time == "10:30")
                ds.TenThirty = true;
            else if (time == "11:00")
                ds.Eleven = true;
            else if (time == "11:30")
                ds.ElevenThirty = true;
            else if (time == "12:00")
                ds.Twelve = true;
            else if (time == "12:30")
                ds.TwelveThirty = true;
            else if (time == "1:00")
                ds.One = true;
            else if (time == "1:30")
                ds.OneThirty = true;
            else if (time == "2:00")
                ds.Two = true;
            else if (time == "2:30")
                ds.TwoThirty = true;
            else if (time == "3:00")
                ds.Three = true;
            else if (time == "3:30")
                ds.ThreeThirty = true;
            else if (time == "4:00")
                ds.Four = true;
            else if (time == "4:30")
                ds.FourThirty = true;

            _context.SaveChanges();

            return Json(new { success = false, errorMessage = "" });
        }


        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Job,Name,Services")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }
        
        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Job,Name,Services")] Worker worker)
        {
            if (id != worker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.ID))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await _context.Worker.FindAsync(id);
            _context.Worker.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
            return _context.Worker.Any(e => e.ID == id);
        }
    }
}
