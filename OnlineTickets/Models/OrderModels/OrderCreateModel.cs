namespace OnlineTickets.Models.OrderModels
{
    public record OrderCreateModel
    {
        public string Hall { get; set; }
        public DateTime FilmStart { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public float Price { get; set; }
        public Guid UserId { get; set; }
        public Guid FilmId { get; set; }
        public Guid CardId { get; set; }
    }
}
