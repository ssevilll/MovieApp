using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoveiApp.Business.DTOs.DirectorDtos;
using MoveiApp.Business.Interfaces;
using MoveiApp.Business.Profiles;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Services
{
    public class DirectorService(
        MovieAppDbContext context,
        IMapper mapper) : IDirectorService
    {

        public async Task<List<DirectorReturnDto>> GetAllDirectorsAsync()
        {
            var directors= await context.Directors
                .Include("Movies")
                .ToListAsync();
            var directorReturnDtos=mapper.Map<List<DirectorReturnDto>>(directors);
            return directorReturnDtos;

        }


        public async Task<DirectorReturnDto> GetDirectorByIdAsync(int id)
        {
            var existing = await context.Directors.FirstOrDefaultAsync(d => d.Id == id);
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
            var directors= await context.Directors
                .Where(d => d.Name.Contains(value))
                .ToListAsync();
            return mapper.Map<List<DirectorReturnDto>>(directors);
        }

        public async Task AddDirectorAsync(DirectorCreateDto directordto)
        {
            if (await context.Directors.AnyAsync(d => d.Name == directordto.Name))
                throw new Exception();
            var director = mapper.Map<Director>(directordto);
            await context.Directors.AddAsync(director);
            await context.SaveChangesAsync();
        }
        public async Task UpdateDirectorAsync(int id, DirectorUpdateDto directorupdatedto)
        {
            if (id != directorupdatedto.Id)
                throw new Exception();
            var existing = await context.Directors.FirstOrDefaultAsync(d => d.Id == id);
            if (existing == null)
                throw new Exception();
            if (context.Directors.Any(d => d.Name == directorupdatedto.Name && d.Id != id))
                throw new Exception();
            mapper.Map(directorupdatedto, existing);
            await context.SaveChangesAsync();
        }
        public async Task DeleteDirectorAsync(int id)
        {
            var existing = await context.Directors.FirstOrDefaultAsync(d => d.Id == id);
            if (existing == null)
                throw new Exception();
            context.Directors.Remove(existing);
            await context.SaveChangesAsync();
        }
    }
}
