using OnlineTickets.Entities;

namespace OnlineTickets.Models.OrderModels
{
    public record OrderCreateModel
    {
        public float TotalPrice { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }

    }
}
