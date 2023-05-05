namespace OnlineTickets.Models.TicketModels
{
    public record TicketUpdateModel: TicketCreateModel
    {
        public Guid Id { get; set; }
    }
}
