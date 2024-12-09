using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarRentalStudio.Models;
using CarRentalStudio.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentalStudio.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Ta metoda ustawia ViewData["IsAdmin"] dla wszystkich akcji w tym kontrolerze
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Sprawdza, czy użytkownik ma rolę "Administrator"
            ViewData["IsAdmin"] = User.IsInRole("Administrator");
            base.OnActionExecuting(context);
        }
        public IActionResult Index()
        {
            return View(); 
        }
        // Wyświetlenie listy samochodów
        public async Task<IActionResult> Cars()
        {
            var cars = await _context.Cars.ToListAsync();
            return PartialView("_CarList", cars);
        }
        // Dodanie nowego samochodu (GET)
        public IActionResult CreateCar()
        {
            return View();
        }
        // Dodanie nowego samochodu (POST)
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Cars));
            }
            return View(car);
        }
        // Edycja samochodu (GET)
        public async Task<IActionResult> EditCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        // Edycja samochodu (POST)
        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Cars));
            }
            return View(car);
        }

        // Usunięcie samochodu
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cars));
        }
        // wyświetlenie użytkowników
        public async Task<IActionResult> Clients()
        {
            var clients = await _context.Clients.ToListAsync();
            return PartialView("_UserList", clients);
        }
        // Dodanie użytkownika (GET)
        public IActionResult CreateClient()
        {
            return View();
        }
        //Dodanie użytkownika (POST)
        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Clients));
            }
            return View(client);
        }
        // Edycja użytkownika (GET)
        public async Task<IActionResult> EditClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
        //Edycja użytkownika (POST)
        [HttpPost]
        public async Task<IActionResult> EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Clients));
            }
            return View(client);
        }
        //Usunięcie użytkownika (GET)
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Clients));
        }
        // Wyświetlenie listy wypożyczeń
        public async Task<IActionResult> Rentals()
        {
            var rentals = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Client)
                .ToListAsync();
            return PartialView("_RentalList", rentals);
        }
        // Dodanie listy wypożyczeń (GET)
        public IActionResult CreateRental()
        {
            return View();
        }
        // Dodanie listy wypożyczeń (POST)
        [HttpPost]
        public async Task<IActionResult> CreateRental(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Rentals));
            }
            return View(rental);
        }
        // Edycja listy wypożyczeń (GET)
        public async Task<IActionResult> EditRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }
        // Edycja listy wypożyczeń (POST)
        [HttpPost]
        public async Task<IActionResult> EditRental(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Rentals));
            }
            return View(rental);
        }
        // Usunięcie wypożyczenia
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Rentals));
        }
    }
}
