using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Model;

namespace ToDoAPI.Data
{
    public class ToDoAPIContext : DbContext
    {
        public ToDoAPIContext (DbContextOptions<ToDoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoTask> ToDoTask { get; set; } = default!;
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User 
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "",
                    Username = "System",
                    Password = "System",
                });
        }
    }
}
