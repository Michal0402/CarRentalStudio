using CarRentalStudio.Models;

public class CarFilterViewModel
{
    public List<string> Brands { get; set; }
    public List<Car> Cars { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
}

