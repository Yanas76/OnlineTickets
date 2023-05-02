namespace OnlineTickets.Entities
{
    public record User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set;}

        public ICollection<Order> Orders = new List<Order>();
    }
}
