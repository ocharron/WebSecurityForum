using FluentValidation;
using Forum.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models.Account
{
    public class RegisterVM
    {
        [MaxLength(50)]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [MaxLength(50)]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? PasswordConfirmation { get; set; }

        public class Validator : AbstractValidator<RegisterVM>
        {
            public Validator()
            {
                RuleFor(u => u.UserName)
                    .NotEmpty()
                        .WithMessage("Username cannot be empty.")
                    .Length(5, 20)
                        .WithMessage("Username must be between 5 and 20 caracters.")
                    .Matches(Constantes.USERNAME_REGEX)
                        .WithMessage("Please provide a valid username.");

                RuleFor(u => u.Password)
                    .NotEmpty()
                        .WithMessage("The Password cannot be empty.")
                    .MinimumLength(Constantes.LENGTH_MIN)
                        .WithMessage($"The Password must have at least {Constantes.LENGTH_MIN} characters.")
                    .Matches(Constantes.REGEX_UPPERCASE)
                        .WithMessage("The Password must have at least one uppercase letter.")
                    .Matches(Constantes.REGEX_LOWERCASE)
                        .WithMessage("The Password must have at least one lowercase letter.")
                    .Matches(Constantes.REGEX_DIGIT)
                        .WithMessage("The Password must have at least one digit.")
                    .Matches(Constantes.REGEX_NO_SPACE)
                        .WithMessage("The Password cannot contain spaces.");

                RuleFor(u => u.PasswordConfirmation)
                    .NotEmpty()
                        .WithMessage("Please confirm the password.")
                    .Equal(vm => vm.Password)
                        .WithMessage("The password and confirmation password do not match.");
            }
        }
    }
}
