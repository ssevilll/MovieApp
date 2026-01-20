using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoveiApp.Business.DTOs.DirectorDtos;
using MoveiApp.Business.Interfaces;
using MoveiApp.Business.Profiles;
using MovieApp.DataAccess.Concretes;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Interfaces;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Services
{
    public class DirectorService(
        IRepository<Director> directorRepository,
        IRepository<Movie> movieRepository,
        IMapper mapper) : IDirectorService
    {

        public async Task<List<DirectorReturnDto>> GetAllDirectorsAsync()
        {
            var directors= await directorRepository.GetAll(false,null,"Movies")
                .ToListAsync();
            var directorReturnDtos=mapper.Map<List<DirectorReturnDto>>(directors);
            return directorReturnDtos;

        }


        public async Task<DirectorReturnDto> GetDirectorByIdAsync(int id)
        {
            var existing = await directorRepository.GetAll(false,d=>d.Id==id,"Movies")
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new Exception();
            var res = mapper.Map<DirectorReturnDto>(existing);
            return res;
        }

        public async Task<List<DirectorReturnDto>> GetAllDirectorsSearchAsync(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception();
            }
            var directors= await directorRepository.GetAll(false,d=>d.Name.Contains(value),"Movies")
                .ToListAsync();
            return mapper.Map<List<DirectorReturnDto>>(directors);
        }

        public async Task AddDirectorAsync(DirectorCreateDto directordto)
        {
            if (await directorRepository.isExistAsync(d => d.Name == directordto.Name))
                throw new Exception();
            var director = mapper.Map<Director>(directordto);
            await directorRepository.AddAsync(director);
            await directorRepository.SaveChangesAsync();
        }
        public async Task UpdateDirectorAsync(int id, DirectorUpdateDto directorupdatedto)
        {
            if (id != directorupdatedto.Id)
                throw new Exception();
            var existing = await directorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception();
            if (await directorRepository.isExistAsync(d => d.Name == directorupdatedto.Name && d.Id != id))
                throw new Exception();
            mapper.Map(directorupdatedto, existing);
            await directorRepository.SaveChangesAsync();
        }
        public async Task DeleteDirectorAsync(int id)
        {
            var existing = await directorRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception();
            directorRepository.Delete(existing);
            await directorRepository.SaveChangesAsync();
        }
    }
}
