namespace Forum.Models.Home
{
    public class CommentaireVM
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Contenu { get; set; }
        public required DateTime Date { get; set; }
    }
}
