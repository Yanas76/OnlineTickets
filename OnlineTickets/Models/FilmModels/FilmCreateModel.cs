using OnlineTickets.Entities;

namespace OnlineTickets.Models.FilmModels
{
    public record FilmCreateModel
    {
        public string Name { get; set; }
        public float Rating { get; set; }
        public float Length { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public Guid CinemaId { get; set; }
    }
}
