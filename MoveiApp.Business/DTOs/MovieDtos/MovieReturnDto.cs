using MovieApp.DataAccess.Models;

namespace MoveiApp.Business.DTOs.MovieDtos
{
    public class MovieReturnDto
    {
        public string Title { get; set; } = null!;
        public DateTime ReleaseYear { get; set; }
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public decimal Imdb { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
