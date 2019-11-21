using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace cs4540_final_project.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext (DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<cs4540_final_project.Models.WorkerComment> WorkerComment { get; set; }
        public DbSet<cs4540_final_project.Models.Worker> Worker { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkerComment>().ToTable("WorkerComment");
            modelBuilder.Entity<Worker>().ToTable("Worker");
        }
    }
}
