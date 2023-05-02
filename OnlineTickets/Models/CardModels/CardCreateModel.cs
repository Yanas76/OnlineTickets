namespace OnlineTickets.Models.CardModels
{
    public record CardCreateModel
    {
        public string Owner { get; set; }
        public string Number { get; set; }
        public DateTime Validity { get; set; }
        public string CVV { get; set; }
    }
}
