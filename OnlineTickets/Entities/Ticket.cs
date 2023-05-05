namespace OnlineTickets.Entities
{
    public record Ticket
    {
        public Guid Id { get; set; }
        public string Hall { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public float Price { get; set; }
        public DateTime FilmStart { get; set; }
        public Film Film { get; set; }
        public Guid FilmId { get; set; }
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
    }
}
