using OnlineTickets.Entities;

namespace OnlineTickets.Models.TicketModels
{
    public record TicketCreateModel
    {
        public string Hall { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public float Price { get; set; }
        public DateTime FilmStart { get; set; }
        public Guid FilmId { get; set; }
        public Guid OrderId { get; set; }
    }
}
