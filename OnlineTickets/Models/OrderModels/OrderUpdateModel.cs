namespace OnlineTickets.Models.OrderModels
{
    public record OrderUpdateModel: OrderCreateModel
    {
        public Guid Id { get; set; }
    }
}
