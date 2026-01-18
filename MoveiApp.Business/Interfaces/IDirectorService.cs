using MoveiApp.Business.DTOs.DirectorDtos;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Interfaces
{
    public interface IDirectorService
    {
        Task AddDirectorAsync(DirectorCreateDto directordto);
        Task<List<DirectorReturnDto>> GetAllDirectorsAsync();
        Task<List<DirectorReturnDto>> GetAllDirectorsSearchAsync(string value);
        Task<DirectorReturnDto> GetDirectorByIdAsync(int id);
        Task UpdateDirectorAsync(int id, DirectorUpdateDto directorupdatedto);
        Task DeleteDirectorAsync(int id);
    }
}