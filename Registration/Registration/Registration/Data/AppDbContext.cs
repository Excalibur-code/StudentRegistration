using Microsoft.EntityFrameworkCore;
using Registration.Models;

namespace Registration.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students_Reg { get; set; }
        public DbSet<State> StatesTbl { get; set; }
        public DbSet<City> CitiesTbl { get; set; } 
    }
}
