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
        [Range(0.0, float.MaxValue, ErrorMessage = "Wartość musi być liczbą większą od zera.")]
        public float EngineCapacity { get; set; }

        [Required(ErrorMessage = "Rodzaj paliwa jest wymagany.")]
        public FuelType FuelType { get; set; } 

        [Required(ErrorMessage = "Typ skrzyni biegów jest wymagany.")]
        public TransmissionType Transmission { get; set; }

        [Required(ErrorMessage = "Typ nadwozia jest wymagany.")]
        public BodyType BodyType { get; set; }

        [Required(ErrorMessage = "Wybierz rodzaj napędu")]
        public Drive Drive { get; set; }

        [Required(ErrorMessage = "Podaj przyśpieszenie 0-100")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Wartość musi być liczbą większą od zera.")]
        public double Acceleration { get; set; }

        [Required(ErrorMessage = "Podaj prędkość maksymalną")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Wartość musi być liczbą większą od zera.")]
        public double VMax { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(0.0, double.MaxValue, ErrorMessage = "Wartość musi być liczbą większą od zera.")]
        public decimal DailyRate { get; set; }
        public bool IsAvailable => Rentals.All(r => r.RentalEnd < DateTime.Now);
        public string Image { get; set; } = "https://staging.simple.tn/wp-content/uploads/2024/07/default.png";
        public ICollection<Rental> Rentals { get; set; }

        // Konstruktor
        public Car()
        {
            Rentals = new List<Rental>();
            FuelType = FuelType.Benzyna; 
            Transmission = TransmissionType.Manualna; 
            BodyType = BodyType.Sedan; 
            Drive = Drive.RWD;
        }

    }

    public enum Drive
    {
        FWD,
        RWD,
        AWD
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
