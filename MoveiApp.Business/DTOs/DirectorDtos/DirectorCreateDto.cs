namespace MoveiApp.Business.DTOs.DirectorDtos
{
    public class DirectorCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? City { get; set; }
        public int Age { get; set; }
    }
}
