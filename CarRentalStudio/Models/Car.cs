namespace CarRentalStudio.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly Year { get; set; }
        public float Mileage { get; set; }
        public int HorsePower { get; set; }
        public bool IsRented{ get; set;} // sprawdzić poźniej czy będzie działać i czy lepiej zrobić w kalendarzu
        public int Price { get; set; }

        //public virtual ICollection<Rent> Rents { get; set; }

    }
}
