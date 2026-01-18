using AutoMapper;
using MoveiApp.Business.DTOs.DirectorDtos;
using MoveiApp.Business.DTOs.MovieDtos;
using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //          source           destination
            CreateMap<DirectorCreateDto, Director>();
            CreateMap<Director, DirectorReturnDto>()
                .ForMember(dest=>dest.Movies,opt=>opt.MapFrom(src=>src.Movies!=null? src.Movies.Select(m=>m.Title).ToList() : new List<string>()));
            CreateMap<DirectorUpdateDto, Director>();

            CreateMap<Movie, MovieReturnDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>();
        }
    }

}
