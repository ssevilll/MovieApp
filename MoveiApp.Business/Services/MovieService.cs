using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MoveiApp.Business.DTOs.MovieDtos;
using MoveiApp.Business.Interfaces;
using MovieApp.DataAccess.Concretes;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Interfaces;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Services
{
    public class MovieService(
        IRepository<Movie> movieRepo,
        IRepository<Director> directorRepo,
        IMapper mapper
        ) : IMovieService
    {
        public async Task<List<MovieReturnDto>> GetAllMoviesAsync()
        {
            return await movieRepo.GetAll()
                .ProjectTo<MovieReturnDto>(mapper.ConfigurationProvider)
                .ToListAsync();

        }
        public async Task<MovieReturnDto> GetMovieByIdAsync(int id)
        {
            var movie = await movieRepo.GetByIdAsync(id, false, "Director");
            if (movie == null)
                throw new Exception("Movie not found");
            return mapper.Map<MovieReturnDto>(movie);
        }
        public async Task<List<MovieReturnDto>> GetMoviesByDirectorAsync(int directorId)
        {
            var movieReturnDtos = await movieRepo.GetAll(false,m=>m.DirectorId==directorId,"Director")
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
            var movieReturnDtos = await movieRepo.GetAll(false,m => m.Title.Contains(title) || m.Description.Contains(title),"Director")
                .ProjectTo<MovieReturnDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            if (movieReturnDtos.Count == 0)
                throw new Exception("No movies found matching the search criteria");
            return movieReturnDtos;
        }
        public async Task AddMovieAsync(MovieCreateDto movieCreateDto)
        {
            if (await movieRepo.isExistAsync(m => m.Title == movieCreateDto.Title))
                throw new Exception("Movie with the same title already exists");
            var directorExists = await directorRepo.isExistAsync(d => d.Id == movieCreateDto.DirectorId);
            if (!directorExists)
                throw new Exception("Director does not exist");
            var movie = mapper.Map<Movie>(movieCreateDto);
            await movieRepo.AddAsync(movie);
            await movieRepo.SaveChangesAsync();
        }
        public async Task UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto)
        {
            if (id != movieUpdateDto.Id)
                throw new Exception("Movie ID mismatch");
            var existingMovie = await movieRepo.GetByIdAsync(id);
            if (existingMovie == null)
                throw new Exception("Movie not found");
            if (await movieRepo.isExistAsync(m => m.Title == movieUpdateDto.Title && m.Id != id))
                throw new Exception("Another movie with the same title already exists");
            var directorExists = await directorRepo.isExistAsync(d => d.Id == movieUpdateDto.DirectorId);
            if (!directorExists)
                throw new Exception("Director does not exist");
            mapper.Map(movieUpdateDto, existingMovie);
            await movieRepo.SaveChangesAsync();

        }
        public async Task DeleteMovieAsync(int id)
        {
            var existingMovie = await movieRepo.GetByIdAsync(id);
            if (existingMovie == null)
                throw new Exception("Movie not found");
            movieRepo.Delete(existingMovie);
            await movieRepo.SaveChangesAsync();
        }
    }
}
