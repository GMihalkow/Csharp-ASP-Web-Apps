using DataServices.Common;
using SimpleShop.DataServices.Models.Interfaces.Account;
using System.ComponentModel.DataAnnotations;

namespace SimpleShop.DataServices.Models.Account
{
    public class LoginInputModel : ILoginInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        public string Username { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.MaxUsernameLength, ErrorMessage = ViewModelsConstants.SharedLengthErrorMessage, MinimumLength = ViewModelsConstants.MinUsernameLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}