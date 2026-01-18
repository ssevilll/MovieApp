using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Models;

namespace MovieApp.DataAccess.Data
{
    public class MovieAppDbContext : DbContext
    {
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
        {
        }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\MAIN;Database=MovieAppDb;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
