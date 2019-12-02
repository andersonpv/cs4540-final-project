using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cs4540_final_project.Data;
using cs4540_final_project.Models;

namespace cs4540_final_project.Views.Comments
{
    public class EditModel : PageModel
    {
        private readonly cs4540_final_project.Data.WorkerContext _context;

        public EditModel(cs4540_final_project.Data.WorkerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkerComment WorkerComment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkerComment = await _context.WorkerComment.FirstOrDefaultAsync(m => m.WorkerCommentID == id);

            if (WorkerComment == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkerComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerCommentExists(WorkerComment.WorkerCommentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkerCommentExists(int id)
        {
            return _context.WorkerComment.Any(e => e.WorkerCommentID == id);
        }
    }
}
