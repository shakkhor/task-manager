using Microsoft.EntityFrameworkCore;
using taskAPI.Models;

namespace taskAPI.DataContext
{
    public class TasksDataContext : DbContext
    {
        public TasksDataContext(DbContextOptions<TasksDataContext> options):
             base(options) 
        { 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<TaskDetail> TaskDetails { get; set; }
    }
}
