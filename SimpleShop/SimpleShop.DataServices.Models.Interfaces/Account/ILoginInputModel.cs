namespace SimpleShop.DataServices.Models.Interfaces.Account
{
    public interface ILoginInputModel
    {
        string Username { get; set; }

        string Password { get; set; }
    }
}