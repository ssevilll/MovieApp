using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoveiApp.Business.DTOs.MovieDtos;
using MoveiApp.Business.Interfaces;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Services
{
    public class MovieService(MovieAppDbContext context, IMapper mapper) : IMovieService
    {
        public async Task<List<MovieReturnDto>> GetAllMoviesAsync()
        {
            return await context.Movies
                .ProjectTo<MovieReturnDto>(mapper.ConfigurationProvider)
                .ToListAsync();

        }
        public async Task<MovieReturnDto> GetMovieByIdAsync(int id)
        {
            var movieReturnDto = await context.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieReturnDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (movieReturnDto == null)
                throw new Exception("Movie not found");
            return movieReturnDto;
        }
        public async Task<List<MovieReturnDto>> GetMoviesByDirectorAsync(int directorId)
        {
            var movieReturnDtos = await context.Movies
                .Where(m => m.DirectorId == directorId)
                .ProjectTo<MovieReturnDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            if (movieReturnDtos.Count == 0)
                throw new Exception("No movies found for the given director");
            return movieReturnDtos;
        }
        public async Task<List<MovieReturnDto>> SearchMoviesAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new Exception("Search title cannot be empty");
            var movieReturnDtos = await context.Movies
                .Where(m => m.Title.Contains(title) || m.Description.Contains(title))
                .ProjectTo<MovieReturnDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            if (movieReturnDtos.Count == 0)
                throw new Exception("No movies found matching the search criteria");
            return movieReturnDtos;
        }
        public async Task AddMovieAsync(MovieCreateDto movieCreateDto)
        {
            if (await context.Movies.AnyAsync(m => m.Title == movieCreateDto.Title))
                throw new Exception("Movie with the same title already exists");
            var directorExists = await context.Directors.AnyAsync(d => d.Id == movieCreateDto.DirectorId);
            if (!directorExists)
                throw new Exception("Director does not exist");
            var movie = mapper.Map<Movie>(movieCreateDto);
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }
        public async Task UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto)
        {
            if (id != movieUpdateDto.Id)
                throw new Exception("Movie ID mismatch");
            var existingMovie = await context.Movies.FindAsync(id);
            if (existingMovie == null)
                throw new Exception("Movie not found");
            if (await context.Movies.AnyAsync(m => m.Title == movieUpdateDto.Title && m.Id != id))
                throw new Exception("Another movie with the same title already exists");
            var directorExists = await context.Directors.AnyAsync(d => d.Id == movieUpdateDto.DirectorId);
            if (!directorExists)
                throw new Exception("Director does not exist");
            mapper.Map(movieUpdateDto, existingMovie);
            await context.SaveChangesAsync();

        }
        public async Task DeleteMovieAsync(int id)
        {
            var existingMovie = await context.Movies.FindAsync(id);
            if (existingMovie == null)
                throw new Exception("Movie not found");
            context.Movies.Remove(existingMovie);
            await context.SaveChangesAsync();
        }
    }
}
