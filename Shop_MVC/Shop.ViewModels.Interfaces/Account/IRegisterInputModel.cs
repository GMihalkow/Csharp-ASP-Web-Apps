namespace Shop.ViewModels.Interfaces.Account
{
    public interface IRegisterInputModel
    {
        string Username { get; set; }

        string Password { get; set; }

        string Email { get; set; }

        string ConfirmPassword { get; set; }

        int Age{ get; set; }
    }
}