using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.DataAccess.Models;

namespace MovieApp.DataAccess.Configurations
{
    public class DirectorConfigurations : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.ToTable("Directors");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(d => d.Address)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(d => d.City)
                .HasMaxLength(100);
            builder.Property(d => d.Age)
                .IsRequired();
            builder.HasIndex(d => d.Name)
                .IsUnique(); //unique constraint on Name

            //seed data
            builder.HasData (
                new Director
                {
                    Id = 1,
                    Name = "Steven Spielberg",
                    Description = "An American filmmaker known for his work in the adventure and science fiction genres.",
                    Address = "123 Hollywood Blvd, Los Angeles, CA",
                    City = "Los Angeles",
                    Age = 74
                },
                new Director
                {
                    Id = 2,
                    Name = "Christopher Nolan",
                    Description = "A British-American filmmaker known for his complex storytelling and innovative techniques.",
                    Address = "456 Sunset St, Los Angeles, CA",
                    City = "Los Angeles",
                    Age = 50
                },
                new Director
                {
                    Id = 3,
                    Name = "Quentin Tarantino",
                    Description = "An American filmmaker known for his stylized violence and sharp dialogue.",
                    Address = "789 Vine St, Los Angeles, CA",
                    City = "Los Angeles",
                    Age = 58
                },
                new Director
                {
                    Id = 4,
                    Name = "Martin Scorsese",
                    Description = "An American filmmaker known for his work on crime films and character studies.",
                    Address = "321 Hollywood Blvd, New York, NY",
                    City = "New York",
                    Age = 78
                }
            );
        }
    }
}
