using System;
using System.ComponentModel.DataAnnotations;
using DataServices.Common;
using SimpleShop.DataServices.Models.Interfaces.Account;

namespace SimpleShop.DataServices.Models.Account
{
    public class RegisterInputModel : IRegisterInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        public string Username { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "ConfirmPassword", ErrorMessage = ViewModelsConstants.PasswordsEqualityErrorMessage)]
        public string Password { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        [DataType(DataType.Password)]
        [Compare(otherProperty: "Password", ErrorMessage = ViewModelsConstants.PasswordsEqualityErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}