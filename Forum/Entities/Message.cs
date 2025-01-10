using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public required string Contenu { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }

        // Navigation properties.
        [ForeignKey(nameof(UserId))]
        public required virtual User User { get; set; }
        public List<Commentaire>? Commentaires { get; set; } = new();
    }
}
