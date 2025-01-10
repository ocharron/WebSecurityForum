using Forum.Entities;

namespace Forum.Models.Home
{
    public class MessageVM
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Contenu { get; set; }
        public required DateTime Date { get; set; }
        public List<CommentaireVM>? Commentaires { get; set; } = new();
    }
}
