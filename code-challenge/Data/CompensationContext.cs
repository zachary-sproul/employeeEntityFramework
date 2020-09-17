using challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace challenge.Data
{
    public class CompensationContext : DbContext
    {
        public CompensationContext(DbContextOptions<CompensationContext> options) : base(options)
        {

        }

        public DbSet<Compensation> Compensations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compensation>().Property<string>("EmployeeId");
            modelBuilder.Entity<Compensation>().HasKey("EmployeeId");
            //EffDate should probably be a key too, but I'm not going to go beyond the task specifications
        }
    }
}
