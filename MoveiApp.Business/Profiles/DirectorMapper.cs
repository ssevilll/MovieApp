using Azure.Core;
using MoveiApp.Business.DTOs.DirectorDtos;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Profiles
{
    public class DirectorMapper
    {
        public static DirectorReturnDto ToDirectorReturnDto (Director director)
        {
            return new DirectorReturnDto
            {
                Name = director.Name,
                Description = director.Description,
                Address = director.Address,
                City = director.City,
                Age = director.Age
            };
        }
        public static Director ToDirector (DirectorCreateDto directorCreateDto)
        {
            return new Director
            {
                Name = directorCreateDto.Name,
                Description = directorCreateDto.Description,
                Address = directorCreateDto.Address,
                City = directorCreateDto.City,
                Age = directorCreateDto.Age
            };
        }
    }
}
