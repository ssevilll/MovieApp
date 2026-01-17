using AutoMapper;
using MoveiApp.Business.DTOs.DirectorDtos;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //          source           destination
            CreateMap<DirectorCreateDto, Director>();
            CreateMap<Director, DirectorReturnDto>();
        }
    }

}
