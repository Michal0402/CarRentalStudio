using CarRentalStudio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static System.Net.WebRequestMethods;

namespace CarRentalStudio.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rental>()
             .HasOne(r => r.Car)
             .WithMany(c => c.Rentals)
             .HasForeignKey(r => r.CarId);
            modelBuilder.Entity<Car>().HasData(
                new Car { Id= 1, Brand="Ferrari", Model="296 GTB", Year=2024, Mileage=1000,HorsePower=830,Torque=740,EngineCapacity=3,FuelType=FuelType.Benzyna,Transmission=TransmissionType.Automatyczna,BodyType=BodyType.Coupe,Drive=Drive.RWD, Acceleration=2.9,VMax=330,DailyRate=1500,Image="https://cylindersi.pl/wp-content/uploads/2023/08/Ferrari-296-GTB-Sylwetka.jpg"},
                new Car { Id= 2, Brand="Ferrari", Model="812 Superfast", Year=2024, Mileage=1000,HorsePower=800,Torque=718,EngineCapacity=6,FuelType=FuelType.Benzyna,Transmission=TransmissionType.Automatyczna,BodyType=BodyType.Coupe,Drive=Drive.RWD,Acceleration=2.9,VMax=340,DailyRate=1500,Image= "https://cylindersi.pl/wp-content/uploads/2024/04/Ferrari-812-SUPERFAST-sylwetka.jpg"},
                new Car { Id= 3, Brand="Land Rover Range Rover", Model="L460", Year=2024, Mileage = 1000, HorsePower=530,Torque=750,EngineCapacity=4,FuelType = FuelType.Diesel,Transmission=TransmissionType.Automatyczna,BodyType= BodyType.SUV,Drive=Drive.AWD,Acceleration=4.6,VMax=260,DailyRate=1165,Image= "https://cylindersi.pl/wp-content/uploads/2024/11/Land-Rover-Range-Rover-SV-sylwetka.jpg"},
                new Car { Id= 4, Brand = "Porsche", Model = "911 Carrera 4 GTS (992)", Year = 2024, Mileage = 1000, HorsePower = 480, Torque = 570, EngineCapacity = 3, FuelType = FuelType.Benzyna, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.Coupe, Drive = Drive.AWD, Acceleration = 3.3, VMax = 311, DailyRate=1130,Image= "https://cylindersi.pl/wp-content/uploads/2022/05/911-Carrera-4-GTS-5-sylwetka.jpg"},
                new Car { Id= 5, Brand = "Audi", Model = "R8 Coupe V10 Performance Quattro", Year = 2024, Mileage = 1000, HorsePower = 620, Torque = 580, EngineCapacity = 5, FuelType = FuelType.Benzyna, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.Coupe, Drive = Drive.AWD, Acceleration = 3.1, VMax = 331, DailyRate=1130,Image= "https://cylindersi.pl/wp-content/uploads/2023/10/Audi-R8-sylwetka.jpg"},
                new Car { Id= 6, Brand = "Mercedes-AMG", Model = "G63", Year = 2024, Mileage = 1000, HorsePower = 585, Torque = 850, EngineCapacity = 4, FuelType = FuelType.Benzyna, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.SUV, Drive = Drive.AWD, Acceleration = 4.5, VMax = 220, DailyRate=965,Image= "https://cylindersi.pl/wp-content/uploads/2022/10/Mercedes-AMG-G63-sylwetka.jpg"},
                new Car { Id= 7, Brand = "BMW", Model = "XM", Year = 2024, Mileage = 1000, HorsePower = 653, Torque = 800, EngineCapacity = 4, FuelType = FuelType.Hybrydowy, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.SUV, Drive = Drive.AWD, Acceleration = 4.3, VMax = 250, DailyRate=930,Image= "https://cylindersi.pl/wp-content/uploads/2023/10/XM-5-sylwetka.jpg"},
                new Car { Id= 8, Brand = "Audi", Model = "RS6", Year = 2024, Mileage = 1000, HorsePower = 630, Torque = 850, EngineCapacity = 4, FuelType = FuelType.Benzyna, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.Kombi, Drive = Drive.AWD, Acceleration = 3.6, VMax = 305, DailyRate=685,Image= "https://cylindersi.pl/wp-content/uploads/2024/07/AUDI-RS6-sylwetka.jpg"},
                new Car { Id= 9, Brand = "Mercedes-Benz", Model = "S400d Long", Year = 2024, Mileage = 1000, HorsePower = 330, Torque = 700, EngineCapacity = 3, FuelType = FuelType.Diesel, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.Sedan, Drive = Drive.AWD, Acceleration = 5.4, VMax = 250, DailyRate=650,Image= "https://cylindersi.pl/wp-content/uploads/2023/04/S-Klasa-5-sylwetka.jpg"},
                new Car { Id= 10, Brand = "BMW", Model = "G70 740d xDrive", Year = 2024, Mileage = 1000, HorsePower = 340, Torque = 700, EngineCapacity = 3, FuelType = FuelType.Diesel, Transmission = TransmissionType.Automatyczna, BodyType = BodyType.Sedan, Drive = Drive.AWD, Acceleration = 5.0, VMax = 250, DailyRate=615,Image= "https://cylindersi.pl/wp-content/uploads/2023/05/BMW-740D-xDrive-Sylwetka.jpg"}
                );
            Console.WriteLine("OnModelCreating was called!");
        }
    }
}
