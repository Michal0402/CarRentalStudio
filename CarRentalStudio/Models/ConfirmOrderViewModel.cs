namespace CarRentalStudio.Models
{
    public class ConfirmOrderViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RentalDays => (EndDate - StartDate).Days; // Automatyczne obliczenie liczby dni
        public decimal TotalPrice { get; set; } // Całkowity koszt wypożyczenia
        public int CarId { get; set; } // Id wybranego samochodu
        public string CarBrand { get; set; } // Marka samochodu
        public string CarModel { get; set; } // Model samochodu
        public decimal PricePerDay { get; set; } // Cena za dzien
    }
}
