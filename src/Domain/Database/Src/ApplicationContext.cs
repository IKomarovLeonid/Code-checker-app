using System.Reflection;
using Database.Configuration;
using Microsoft.EntityFrameworkCore;
using Objects.Dto;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=database.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=database.db");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskDbConfiguration());
            modelBuilder.ApplyConfiguration(new CodeSolutionDbConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CodeTaskDto> Tasks { get; set; }

        public DbSet<CodeSolutionDto> Solutions { get; set; }
    }
}
