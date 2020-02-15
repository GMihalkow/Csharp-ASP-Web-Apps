namespace ShopApp.Web.Models
{
    public class Alert
    {
        public Alert(AlertType alertType, string message)
        {
            this.AlertType = alertType;
            this.Message = message;
        }
        
        public AlertType AlertType { get; set; }

        public string Message { get; set; }
    }
}