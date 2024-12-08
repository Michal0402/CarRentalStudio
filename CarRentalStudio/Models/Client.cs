using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentalStudio.Models
{
    public class Client : IdentityUser
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string PasswordHash { get; set; } 

        [Required]
        public string FullName { get; set; } 

        public string PhoneNumber { get; set; } 

        public ICollection<Rental> Rentals { get; set; } 
    }
}
