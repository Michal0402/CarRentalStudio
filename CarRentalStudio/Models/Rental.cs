using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalStudio.Models
{
    public class Rental
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Client")] public string ClientId { get; set; }
        public IdentityUser? Client { get; set; }

        [ForeignKey("Car")] public int CarId { get; set; }
        public Car? Car { get; set; }

        [Required] 
        public DateTime RentalStart { get; set; }

        [Required]
        [CustomValidation(typeof(Rental), nameof(ValidateRentalPeriod))]
        public DateTime RentalEnd { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Precision(18, 2)]
        public decimal Price => CalculatePrice();

        public bool IsActive => DateTime.Now < RentalEnd && DateTime.Now >= RentalStart;

        //Sprawdzenie czy data RentalEnd jest pod dacie RentalStart
        public static ValidationResult? ValidateRentalPeriod(DateTime rentalEnd, ValidationContext context)
        {
            var instance = context.ObjectInstance as Rental;
            if (instance != null && rentalEnd <= instance.RentalStart)
            {
                return new ValidationResult("Data zakończenia musi być po dacie rozpoczęcia.");
            }

            return ValidationResult.Success;
        }

        private decimal CalculatePrice()
        {
            int rentalDays = (RentalEnd - RentalStart).Days;
            if (rentalDays <= 0)
            {
                throw new InvalidOperationException("Data zakończenia musi być po dacie rozpoczęcia.");
            }

            return rentalDays * Car.DailyRate;

        }
    }
}
