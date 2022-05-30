using Microsoft.EntityFrameworkCore;

namespace MinimalWebAPI.ToDo
{
    public class ToDoDbContext : DbContext
    {
        private string _connectionString = "Server=localhost;Database=ToDoDB;Trusted_Connection=True;";

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
    
