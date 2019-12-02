using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using cs4540_final_project.Data;
using cs4540_final_project.Models;

namespace cs4540_final_project.Views.Comments
{
    public class DeleteModel : PageModel
    {
        private readonly cs4540_final_project.Data.WorkerContext _context;

        public DeleteModel(cs4540_final_project.Data.WorkerContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkerComment = await _context.WorkerComment.FindAsync(id);

            if (WorkerComment != null)
            {
                _context.WorkerComment.Remove(WorkerComment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
