using Microsoft.AspNetCore.Identity;

namespace CarRentalStudio.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        //public virtual Car Car { get; set; }
    }
}
