namespace OnlineTickets.Models.UserModels
{
    public record UserUpdateModel: UserCreateModel
    {
        public Guid Id { get; set; }
    }
}
