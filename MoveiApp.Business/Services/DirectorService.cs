using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoveiApp.Business.DTOs.DirectorDtos;
using MoveiApp.Business.Profiles;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Services
{
    public class DirectorService (
        MovieAppDbContext context,
        IMapper mapper)
    {
        public List<DirectorReturnDto> GetAllDirectors()
        {
            var directors = context.Directors.ToList();
            List<DirectorReturnDto> directorDtos = new List<DirectorReturnDto>();
            foreach (var director in directors)
            {
                var DirectorReturnDto = mapper.Map<DirectorReturnDto>(director);
                directorDtos.Add(DirectorReturnDto);
            };
            return directorDtos;
        }

        public async Task<List<DirectorReturnDto>> GetAllDirectorsAsync() =>
            await context.Directors.Select(d => new DirectorReturnDto
            {
                Name = d.Name,
                Description = d.Description,
                Address = d.Address,
                City = d.City,
                Age = d.Age
            })
            .ToListAsync();

        public DirectorReturnDto GetDirectorById(int id)
        {
            var existing=context.Directors.FirstOrDefault(d => d.Id == id);
            if (existing == null)
                throw new Exception();
            var res= mapper.Map<DirectorReturnDto>(existing);
            return res;
        }

        public async Task<DirectorReturnDto> GetDirectorByIdAsync(int id)
        {
            var existing = await context.Directors.FirstOrDefaultAsync(d => d.Id == id);
            if (existing == null)
                throw new Exception();
            var res = mapper.Map<DirectorReturnDto>(existing);
            return res;
        }

        public List<Director> GetAllDirectorsSearch(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception();
            }
            return context.Directors
                .Where(d => d.Name.Contains(value))
                .ToList();

        }
        public async Task<List<Director>> GetAllDirectorsAsync(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception();
            }
            return await context.Directors
                .Where(d => d.Name.Contains(value))
                .ToListAsync();
        }

        public void Add(DirectorCreateDto directordto)
        {
            if (context.Directors.Any(d => d.Name == directordto.Name))
                throw new Exception();
            var director = mapper.Map<Director>(directordto);
            context.Directors.Add(director);
            context.SaveChanges();
        }

        public async Task AddAsync(DirectorCreateDto directordto)
        {
            if (await context.Directors.AnyAsync(d => d.Name == directordto.Name))
                throw new Exception();
            var director = mapper.Map<Director>(directordto);
            await context.Directors.AddAsync(director);
            await context.SaveChangesAsync();
        }
    }
}
