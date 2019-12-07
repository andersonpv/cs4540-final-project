using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cs4540_final_project.Models;
using cs4540_final_project.Data;
using Microsoft.EntityFrameworkCore;

namespace cs4540_final_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly WorkerContext _context;

        public HomeController(WorkerContext context)
        {
            _context = context;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Worker.ToListAsync());
        }

        public async Task<IActionResult> Overview()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
