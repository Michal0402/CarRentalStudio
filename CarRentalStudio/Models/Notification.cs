using Microsoft.AspNetCore.Identity;

namespace CarRentalStudio.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public void SendNotification() { } 

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
