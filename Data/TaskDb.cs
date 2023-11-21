using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TaskMngr.Models;

namespace TaskMngr.Data
{
    public class TaskDb : DbContext
    {
        public TaskDb(DbContextOptions<TaskDb> options) : base(options)
        {
        }


        public DbSet<Tsk> Tsks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Tsk>().HasData(
                new Tsk { Id = 1, Name = "Task 1", Description = "Description for Task 1" },
                new Tsk { Id = 2, Name = "Task 2", Description = "Description for Task 2" },
                new Tsk { Id = 3, Name = "Task 3", Description = "Description for Task 3" },
                new Tsk { Id = 4, Name = "Task 4", Description = "Description for Task 4" },
                new Tsk { Id = 5, Name = "Task 5", Description = "Description for Task 5" }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}