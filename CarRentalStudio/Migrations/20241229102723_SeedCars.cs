using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalStudio.Migrations
{
    /// <inheritdoc />
    public partial class SeedCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Acceleration", "BodyType", "Brand", "DailyRate", "Drive", "EngineCapacity", "FuelType", "HorsePower", "Image", "Mileage", "Model", "Torque", "Transmission", "VMax", "Year" },
                values: new object[,]
                {
                    { 1, 2.8999999999999999, 4, "Ferrari", 1500m, 1, 3f, 0, 830, "https://cylindersi.pl/wp-content/uploads/2023/08/Ferrari-296-GTB-Sylwetka.jpg", 1000f, "296 GTB", 740, 1, 330.0, 2024 },
                    { 2, 2.8999999999999999, 4, "Ferrari", 1500m, 1, 6f, 0, 800, "https://cylindersi.pl/wp-content/uploads/2024/04/Ferrari-812-SUPERFAST-sylwetka.jpg", 1000f, "812 Superfast", 718, 1, 340.0, 2024 },
                    { 3, 4.5999999999999996, 1, "Land Rover Range Rover", 1165m, 2, 4f, 1, 530, "https://cylindersi.pl/wp-content/uploads/2024/11/Land-Rover-Range-Rover-SV-sylwetka.jpg", 1000f, "L460", 750, 1, 260.0, 2024 },
                    { 4, 3.2999999999999998, 4, "Porsche", 1130m, 2, 3f, 0, 480, "https://cylindersi.pl/wp-content/uploads/2022/05/911-Carrera-4-GTS-5-sylwetka.jpg", 1000f, "911 Carrera 4 GTS (992)", 570, 1, 311.0, 2024 },
                    { 5, 3.1000000000000001, 4, "Audi", 1130m, 2, 5f, 0, 620, "https://cylindersi.pl/wp-content/uploads/2023/10/Audi-R8-sylwetka.jpg", 1000f, "R8 Coupe V10 Performance Quattro", 580, 1, 331.0, 2024 },
                    { 6, 4.5, 1, "Mercedes-AMG", 965m, 2, 4f, 0, 585, "https://cylindersi.pl/wp-content/uploads/2022/10/Mercedes-AMG-G63-sylwetka.jpg", 1000f, "G63", 850, 1, 220.0, 2024 },
                    { 7, 4.2999999999999998, 1, "BMW", 930m, 2, 4f, 4, 653, "https://cylindersi.pl/wp-content/uploads/2023/10/XM-5-sylwetka.jpg", 1000f, "XM", 800, 1, 250.0, 2024 },
                    { 8, 3.6000000000000001, 3, "Audi", 685m, 2, 4f, 0, 630, "https://cylindersi.pl/wp-content/uploads/2024/07/AUDI-RS6-sylwetka.jpg", 1000f, "RS6", 850, 1, 305.0, 2024 },
                    { 9, 5.4000000000000004, 0, "Mercedes-Benz", 650m, 2, 3f, 1, 330, "https://cylindersi.pl/wp-content/uploads/2023/04/S-Klasa-5-sylwetka.jpg", 1000f, "S400d Long", 700, 1, 250.0, 2024 },
                    { 10, 5.0, 0, "BMW", 615m, 2, 3f, 1, 340, "https://cylindersi.pl/wp-content/uploads/2023/05/BMW-740D-xDrive-Sylwetka.jpg", 1000f, "G70 740d xDrive", 700, 1, 250.0, 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
