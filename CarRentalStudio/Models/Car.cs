using System.ComponentModel.DataAnnotations;

namespace CarRentalStudio.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string Brand { get; set; } 

        [Required]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public float Mileage { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public decimal DailyRate { get; set; } 

        public bool IsAvailable { get; set; } 

        public ICollection<Rental> Rentals { get; set; } 
    }
}
