namespace OnlineTickets.Entities
{
    public record Card
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string Number { get; set; }
        public DateTime Validity { get; set; }
        public string CVV { get; set; }

        public ICollection<Order> Orders = new List<Order>();
    }
}
