namespace OnlineTickets.Entities
{
    public record Order
    {
        public Guid Id { get; set; }
        public float TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Ticket> Tickets { get; set; }
             
    }
}
