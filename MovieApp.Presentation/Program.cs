using System;
using System.Threading.Tasks;
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
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            //askbro
            serviceCollection.AddDbContext<MovieAppDbContext>();
            //serviceCollection.AddDbContext<MovieAppDbContext>(options =>
            //    options.UseSqlServer("Server=.\SQLEXPRESS;Database=MovieAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));
            serviceCollection.AddAutoMapper(options =>
            {
                options.AddProfile<MapperProfile>();
            });
            serviceCollection.AddLogging();
            serviceCollection.AddScoped<IDirectorService, DirectorService>();
            serviceCollection.AddScoped<IMovieService, MovieService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var directorService = serviceProvider.GetRequiredService<IDirectorService>();
            var movieService = serviceProvider.GetRequiredService<IMovieService>();

            //director name getirdiyim ucun GetAllMoviesAsync methodda include etmedik
            var movies = await movieService.GetAllMoviesAsync();
            foreach (var movie in movies)
            {
                Console.WriteLine($"Movie: {movie.Title}, Director name: {movie.Director.Name}");
            }


            //namelerden ibaret list getirdiyim ucun GetAllDirectorsAsync methodda include istifade etdik
            var directors = await directorService.GetAllDirectorsAsync();
            foreach (var director in directors)
            {
                Console.WriteLine($"Director: {director.Name}, Movies:{string.Join(",", director.Movies)}");
            }
        }
    }
}
