using Hondrade_CodeFirstERD.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hondrade_CodeFirstERD.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Citizen> Citizens { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Citizen)
                .WithMany(c => c.Applications)
                .HasForeignKey(a => a.CitizenID);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Service)
                .WithMany(c => c.Applications)
                .HasForeignKey(a => a.ServiceID);

            modelBuilder.Entity<Service>()
                .HasOne(a => a.Department)
                .WithMany(c => c.Services)
                .HasForeignKey(a => a.DepID);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Department)
                .WithMany(c => c.Employees)
                .HasForeignKey(a => a.DepID);

            modelBuilder.Entity<Contact>()
                .HasOne(a => a.Employee)
                .WithMany(c => c.Contacts)
                .HasForeignKey(a => a.EmpID);

            modelBuilder.Entity<Contact>()
                .HasOne(a => a.Citizen)
                .WithMany(c => c.Contacts)
                .HasForeignKey(a => a.CitizenID);
        }
    }
}
