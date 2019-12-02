using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cs4540_final_project.Models;

namespace cs4540_final_project.Data
{
    public class WorkerContext : DbContext
    {
        public WorkerContext (DbContextOptions<WorkerContext> options)
            : base(options)
        {
        }

        public DbSet<cs4540_final_project.Models.Worker> Worker { get; set; }
        public DbSet<cs4540_final_project.Models.WorkerComment> WorkerComment { get; set; }
    }
}
