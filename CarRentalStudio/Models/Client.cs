using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentalStudio.Models
{
    public class Client : IdentityUser
    {
        public ICollection<Rental> Rentals { get; set; }
    }
}
