using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User
            {
                UserID = 1,
                Username = "admin",
                Email = "admin@domain.com",
                Password = "ADmin123.", 
                DateOfBirth = DateTime.Now,  
                Role = UserRole.Admin
            });
        }
    }
}
