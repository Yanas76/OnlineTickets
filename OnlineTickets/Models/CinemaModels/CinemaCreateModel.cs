namespace OnlineTickets.Models.CinemaModels
{
    public record CinemaCreateModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
