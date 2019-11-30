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
    public class IndexModel : PageModel
    {
        private readonly cs4540_final_project.Data.WorkerContext _context;

        public IndexModel(cs4540_final_project.Data.WorkerContext context)
        {
            _context = context;
        }

        public IList<WorkerComment> WorkerComment { get;set; }

        public async Task OnGetAsync()
        {
            WorkerComment = await _context.WorkerComment.ToListAsync();
        }
    }
}
