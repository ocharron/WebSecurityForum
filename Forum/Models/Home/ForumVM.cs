namespace Forum.Models.Home
{
    public class ForumVM
    {
        public required List<MessageVM> Messages { get; set; }
        public required NewMessageVM NewMessage { get; set; }
        public required int CurrentPage { get; set; }
        public required bool IsAdmin { get; set; }
        public required bool HasPreviousPage { get; set; }
        public required bool HasNextPage { get; set; }
    }
}
