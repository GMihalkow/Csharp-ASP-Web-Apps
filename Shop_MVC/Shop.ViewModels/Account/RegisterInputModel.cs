using Shop.ViewModels.Interfaces.Account;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels.Account
{
    public class RegisterInputModel : IRegisterInputModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Age { get; set; }
    }
}