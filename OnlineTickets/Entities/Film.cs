namespace OnlineTickets.Entities
{
    public record Film
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public float Length { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public Cinema Cinema { get; set; }
        public Guid CinemaId { get; set; }

        public ICollection<Order> Orders = new List<Order>();
    }
}
