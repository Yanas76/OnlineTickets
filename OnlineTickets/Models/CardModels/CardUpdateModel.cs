namespace OnlineTickets.Models.CardModels
{
    public record CardUpdateModel: CardCreateModel
    {
        public Guid Id { get; set; }
    }
}
