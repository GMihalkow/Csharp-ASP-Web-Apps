using System;

namespace SimpleShop.DataServices.Models.Interfaces.Account
{
    public interface IRegisterInputModel
    {
        string Username { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }

        string Email { get; set; }

        DateTime BirthDate { get; set; }
    }
}