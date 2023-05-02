namespace OnlineTickets.Entities
{
    public record Cinema
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public ICollection<Film> Films = new List<Film>();
    }
}
