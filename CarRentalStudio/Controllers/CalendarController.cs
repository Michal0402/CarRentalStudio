using CarRentalStudio.Data;
using CarRentalStudio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalStudio.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult CarAvailability(int carId, int? year, int? month)
        {
            var car = _context.Cars.Include("Rentals").FirstOrDefault(c => c.Id == carId);

            var currentDate = DateTime.Now;
            int displayYear = year ?? currentDate.Year;
            int displayMonth = month ?? currentDate.Month;

            var calendarDays = GenerateCalendar(displayYear, displayMonth, car);

            ViewBag.Year = displayYear;
            ViewBag.Month = displayMonth;
            ViewBag.Car = car;

            return View(calendarDays);
        }

        private List<Calendar> GenerateCalendar(int year, int month, Car car)
        {
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var calendarDays = new List<Calendar>();

            var rentalsInMonth = car.Rentals.Where(r => r.RentalStart.Month == month && r.RentalStart.Year == year)
                .ToList();

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(year, month, day);
                bool isReserved = rentalsInMonth.Any(r => r.RentalStart <= date && r.RentalEnd >= date);

                calendarDays.Add(new Calendar
                {
                    Date = date,
                    IsReserved = isReserved
                });
            }

            return calendarDays;
        }
    }
}

