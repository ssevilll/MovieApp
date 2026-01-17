using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoveiApp.Business.Profiles;
using MoveiApp.Business.Services;
using MovieApp.DataAccess.Data;
using AutoMapper;

namespace MovieApp.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            //askbro
            //serviceCollection.AddDbContext<MovieAppDbContext>();
            serviceCollection.AddDbContext<MovieAppDbContext>(options =>
                options.UseSqlServer("Server=.\\SQLEXPRESS;Database=MovieAppDb;Trusted_Connection=True;TrustServerCertificate=True;"));
            serviceCollection.AddAutoMapper(options =>
            {
                options.AddProfile<MapperProfile>();
            });
            serviceCollection.AddLogging();
            serviceCollection.AddScoped<DirectorService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var directorService = serviceProvider.GetRequiredService<DirectorService>();
            var directors=directorService.GetAllDirectorsSearch("a");
            foreach (var director in directors)
            {
                Console.WriteLine($"Name: {director.Name}");
            }
        }
    }
}
