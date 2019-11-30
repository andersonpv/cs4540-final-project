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
    public class DetailsModel : PageModel
    {
        private readonly cs4540_final_project.Data.WorkerContext _context;

        public DetailsModel(cs4540_final_project.Data.WorkerContext context)
        {
            _context = context;
        }

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
    }
}
