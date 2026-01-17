using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.DataAccess.Models;

namespace MovieApp.DataAccess.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Duration)
                .IsRequired();
            builder.Property(m => m.Imdb)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(m=>m.Director)
                .WithMany(d=>d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Cascade);

            //seed data
            builder.HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    ReleaseYear = new DateTime(2010, 7, 16),
                    Description = "A thief who steals corporate secrets ",
                    Duration = 148,
                    Imdb = 8.8m,
                    DirectorId = 1
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    ReleaseYear = new DateTime(2008, 7, 18),
                    Description = "When the menace known as the Joker wreaks havoc",
                    Duration = 152,
                    Imdb = 9.0m,
                    DirectorId = 1
                },
                new Movie
                {
                    Id = 3,
                    Title = "Pulp Fiction",
                    ReleaseYear = new DateTime(1994, 10, 14),
                    Description = "The lives of two mob hitmen intertwine.",
                    Duration = 154,
                    Imdb = 8.9m,
                    DirectorId = 2
                },
                new Movie
                {
                    Id = 4,
                    Title = "Django Unchained",
                    ReleaseYear = new DateTime(2012, 12, 25),
                    Description = "With the help of rescue.",
                    Duration = 165,
                    Imdb = 8.4m,
                    DirectorId = 2
                },
                new Movie
                {
                    Id = 5,
                    Title = "The Grand Budapest Hotel",
                    ReleaseYear = new DateTime(2014, 3, 28),
                    Description = "A writer encounters serving",
                    Duration = 99,
                    Imdb = 8.1m,
                    DirectorId = 3
                },
                new Movie
                {
                    Id = 6,
                    Title = "Moonrise Kingdom",
                    ReleaseYear = new DateTime(2012, 6, 29),
                    Description = "A pair of young lovers flee to find them.",
                    Duration = 94,
                    Imdb = 7.8m,
                    DirectorId = 3
                },
                new Movie
                {
                    Id = 7,
                    Title = "The Shawshank Redemption",
                    ReleaseYear = new DateTime(1994, 9, 23),
                    Description = "Two imprisoned",
                    Duration = 142,
                    Imdb = 9.3m,
                    DirectorId = 4
                }
            );
        }
    }
}
