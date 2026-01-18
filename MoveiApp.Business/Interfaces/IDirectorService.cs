using MoveiApp.Business.DTOs.DirectorDtos;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Interfaces
{
    public interface IDirectorService
    {
        void Add(DirectorCreateDto directordto);
        Task AddAsync(DirectorCreateDto directordto);
        List<DirectorReturnDto> GetAllDirectors();
        Task<List<DirectorReturnDto>> GetAllDirectorsAsync();
        Task<List<Director>> GetAllDirectorsAsync(string value);
        List<Director> GetAllDirectorsSearch(string value);
        DirectorReturnDto GetDirectorById(int id);
        Task<DirectorReturnDto> GetDirectorByIdAsync(int id);
    }
}