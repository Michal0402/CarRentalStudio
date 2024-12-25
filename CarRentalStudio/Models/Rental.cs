using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalStudio.Models
{
    public class Rental
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Client"), Required]
        public string ClientId { get; set; }
        public IdentityUser? Client { get; set; }

        [ForeignKey("Car"), Required]
        public int CarId { get; set; }
        public Car? Car { get; set; }

        [Required]
        public DateTime RentalStart { get; set; }

        [Required]
        [CustomValidation(typeof(Rental), nameof(ValidateRentalPeriod))]
        public DateTime RentalEnd { get; set; }

        [Required, Range(0, double.MaxValue), Precision(18, 2)]
        public decimal Price { get; set; }

        public bool IsActive => DateTime.Now >= RentalStart && DateTime.Now < RentalEnd;

        public static ValidationResult? ValidateRentalPeriod(DateTime rentalEnd, ValidationContext context)
        {
            var instance = context.ObjectInstance as Rental;
            if (instance != null && rentalEnd <= instance.RentalStart)
            {
                return new ValidationResult("Data zakończenia musi być po dacie rozpoczęcia.");
            }

            return ValidationResult.Success;
        }

        public decimal CalculatePrice()
        {
            if (Car == null)
                throw new InvalidOperationException("Samochód nie został przypisany.");

            return (RentalEnd - RentalStart).Days * Car.DailyRate;
        }
    }
}
