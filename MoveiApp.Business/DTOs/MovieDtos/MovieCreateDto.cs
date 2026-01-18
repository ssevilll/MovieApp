namespace MoveiApp.Business.DTOs.MovieDtos
{
    public class MovieCreateDto
    {
        public string Title { get; set; } = null!;
        public DateTime ReleaseYear { get; set; }
        public string Description { get; set; } = null!;
        public int Duration { get; set; }
        public decimal Imdb { get; set; }
        public int DirectorId { get; set; }
    }
}
