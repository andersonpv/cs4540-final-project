using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using cs4540_final_project.Data;
using cs4540_final_project.Models;

namespace cs4540_final_project.Views.Comments
{
    public class CreateModel : PageModel
    {
        private readonly cs4540_final_project.Data.WorkerContext _context;

        public CreateModel(cs4540_final_project.Data.WorkerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WorkerComment WorkerComment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WorkerComment.Add(WorkerComment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}