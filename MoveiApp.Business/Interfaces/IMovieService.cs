using MoveiApp.Business.DTOs.MovieDtos;

namespace MoveiApp.Business.Interfaces
{
    public interface IMovieService
    {
        Task AddMovieAsync(MovieCreateDto movieCreateDto);
        Task DeleteMovieAsync(int id);
        Task<List<MovieReturnDto>> GetAllMoviesAsync();
        Task<MovieReturnDto> GetMovieByIdAsync(int id);
        Task<List<MovieReturnDto>> GetMoviesByDirectorAsync(int directorId);
        Task<List<MovieReturnDto>> SearchMoviesAsync(string title);
        Task UpdateMovieAsync(int id, MovieUpdateDto movieUpdateDto);
    }
}