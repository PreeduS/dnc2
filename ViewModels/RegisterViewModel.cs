using System.ComponentModel.DataAnnotations;

namespace dnc2.ViewModels{

    public class RegisterViewModel  {

        [Required]
        [MaxLength(256)]
        public string Email {get; set; }
        [Required]
        [MaxLength(256)]
        public string Password {get; set; }
        [Required]
        [MaxLength(256)]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword {get; set; }
       
    }
}