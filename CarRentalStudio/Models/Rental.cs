using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarRentalStudio.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; } 

        public int ClientId { get; set; }
        public virtual Client Client { get; set; } 

        public int CarId { get; set; }
        public virtual Car Car { get; set; } 

        [Required]
        public DateTime RentalStart { get; set; } 

        [Required]
        public DateTime RentalEnd { get; set; } 

        [Required]
        [Range(0, double.MaxValue)] 
        public decimal Price { get; set; }

        public bool IsActive => DateTime.Now < RentalEnd && DateTime.Now >= RentalStart; 
    }
}
