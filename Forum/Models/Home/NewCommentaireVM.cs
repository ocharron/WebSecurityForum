namespace Forum.Models.Home
{
    public class NewCommentaireVM
    {
        public required string Contenu { get; set; }
        public required Guid MessageId { get; set; }
    }
}
