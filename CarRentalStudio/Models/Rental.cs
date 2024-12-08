using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalStudio.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; } 

        [Required]
        public DateTime RentalStart { get; set; } 

        [Required]
        public DateTime RentalEnd { get; set; } 

        [Required]
        [Range(0, double.MaxValue)]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public bool IsActive => DateTime.Now < RentalEnd && DateTime.Now >= RentalStart; 
    }
}
