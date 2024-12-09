using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentalStudio.Models
{
    public class Admin : IdentityUser
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

        // Do konsultacji
       /* public class Admin : IdentityUser
        {
            [Required]
            public string FullName { get; set; }
        }*/
    }
}
