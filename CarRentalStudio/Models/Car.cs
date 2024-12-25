using Microsoft.EntityFrameworkCore;
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
        [Range(1900, int.MaxValue, ErrorMessage = "Rok musi być większy niż 1900.")]
        public int Year { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Przebieg musi być dodatnią liczbą.")]
        public float Mileage { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Moc silnika musi być dodatnią liczbą.")]
        public int HorsePower { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Moment obrotowy musi być dodatnią liczbą.")]
        public int Torque { get; set; }

        [Required(ErrorMessage = "Pojemność silnika jest wymagana.")]
        [Range(0.5, 10.0, ErrorMessage = "Pojemność silnika musi być w zakresie od 0.5 do 10.0 litrów.")]
        public float EngineCapacity { get; set; }

        [Required(ErrorMessage = "Rodzaj paliwa jest wymagany.")]
        public FuelType FuelType { get; set; } 

        [Required(ErrorMessage = "Typ skrzyni biegów jest wymagany.")]
        public TransmissionType Transmission { get; set; }

        [Required(ErrorMessage = "Typ nadwozia jest wymagany.")]
        public BodyType BodyType { get; set; }

        [Required]
        [Precision(18, 2)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DailyRate { get; set; }
        public bool IsAvailable => Rentals.All(r => r.RentalEnd < DateTime.Now);
        public string Image { get; set; } = "https://staging.simple.tn/wp-content/uploads/2024/07/default.png";
        public ICollection<Rental> Rentals { get; set; }

        // Konstruktor
        public Car()
        {
            Rentals = new List<Rental>();
            FuelType = FuelType.Benzyna; // Domyślny typ paliwa
            Transmission = TransmissionType.Manualna; // Domyślny typ skrzyni biegów
            BodyType = BodyType.Sedan; // Domyślny typ nadwozia
        }

    }

    public enum FuelType
    {
        Benzyna,
        Diesel,
        LPG,
        Elektryczny,
        Hybrydowy
    }

    public enum TransmissionType
    {
        Manualna,
        Automatyczna,
        Półautomatyczna
    }

    public enum BodyType
    {
        Sedan,
        SUV,
        Hatchback,
        Kombi,
        Coupe,
        Cabrio,
        Inny
    }


}
