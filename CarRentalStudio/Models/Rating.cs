using Microsoft.AspNetCore.Identity;

namespace CarRentalStudio.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Mark { get; set;}
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
