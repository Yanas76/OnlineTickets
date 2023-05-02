namespace OnlineTickets.Models.CinemaModels
{
    public record CinemaUpdateModel: CinemaCreateModel
    {
        public Guid Id { get; set; }
    }
}
