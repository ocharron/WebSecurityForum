using System.ComponentModel.DataAnnotations;

namespace Forum.Models.Account
{
    public class LogInVM
    {
        [MaxLength(50)]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [MaxLength(100)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
