namespace OnlineTickets.Models.FilmModels
{
    public record FilmUpdateModel: FilmCreateModel
    {
        public Guid Id { get; set; }
    }
}
