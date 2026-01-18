using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoveiApp.Business.Profiles;
using MoveiApp.Business.Services;
using MovieApp.DataAccess.Data;
using AutoMapper;
using MoveiApp.Business.Interfaces;

namespace MovieApp.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            //askbro
            serviceCollection.AddDbContext<MovieAppDbContext>();
            //serviceCollection.AddDbContext<MovieAppDbContext>(options =>
            //    options.UseSqlServer("Server=SUN07\\MAIN;Database=MovieAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));
            serviceCollection.AddAutoMapper(options =>
            {
                options.AddProfile<MapperProfile>();
            });
            serviceCollection.AddLogging();
            serviceCollection.AddScoped<IDirectorService, DirectorService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var directorService = serviceProvider.GetRequiredService<IDirectorService>();
            var directors = directorService.GetAllDirectorsSearch("a");
            foreach (var director in directors)
            {
                Console.WriteLine($"Name: {director.Name}");
            }
        }
    }
}
