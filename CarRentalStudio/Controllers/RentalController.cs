using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalStudio.Data;
using CarRentalStudio.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentalStudio.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RentalController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Akcja do wyświetlania formularza potwierdzenia
        [HttpGet]
        public IActionResult ConfirmOrder(int carId, DateTime rentalStart, DateTime rentalEnd)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                return NotFound();
            }

            var rental = new Rental
            {
                CarId = carId,
                Car = car,
                RentalStart = rentalStart,
                RentalEnd = rentalEnd,
                ClientId = User.Identity.Name // Załóżmy, że klient jest już zalogowany
            };

            return View(rental);
        }

        // Akcja do zapisu zamówienia
        [HttpPost]
        public IActionResult ConfirmOrder(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Rentals.Add(rental);
                _context.SaveChanges();

                // Przekierowanie do strony z potwierdzeniem
                return RedirectToAction("RentalSuccess");
            }

            return View(rental);
        }

        // Akcja do wyświetlania strony potwierdzenia sukcesu
        public IActionResult RentalSuccess()
        {
            return View();
        }

        

        // GET: Display Calendar
        public IActionResult DisplayCalendar(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car == null || !car.IsAvailable)
            {
                return NotFound("Samochód nie jest dostępny.");
            }

            return View(new OrderViewModel { CarId = carId });
        }

        // POST: Check Availability
        [HttpPost]
        public IActionResult CheckAvailability(int carId, DateTime startDate, DateTime endDate)
        {
            Car? car = _context.Cars.Find(carId);
            if (car == null)
            {
                return NotFound("Samochód nie istnieje.");
            }
            //Sprawdza czy samochód nie jest już wynajety
            bool overlappingRentals = _context.Rentals.Any(r => r.CarId == carId &&
                ((startDate >= r.RentalStart && startDate <= r.RentalEnd) ||
                 (endDate >= r.RentalStart && endDate <= r.RentalEnd) ||
                 (startDate <= r.RentalStart && endDate >= r.RentalEnd)));

            if (overlappingRentals)
            {
                return Json(new { available = false, message = "Samochód jest zajęty w wybranym terminie." });
            }

            return Json(new
            {
                available = true,
                redirectUrl = Url.Action("ConfirmOrder", "Rental",
                    new { carId, rentalStart = startDate, rentalEnd = endDate })
            });
        }

        // POST: Create Order
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("DisplayCalendar", model);
            }

            Car car = _context.Cars.Find(model.CarId);
            if (car == null || !car.IsAvailable)
            {
                return NotFound("Samochód nie jest dostępny.");
            }

            var rental = new Rental
            {
                ClientId = User.Identity.Name,
                CarId = model.CarId,
                Car = car,
                RentalStart = model.RentalStart,
                RentalEnd = model.RentalEnd,
            };

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            return RedirectToAction("Confirmation", new { rentalId = rental.Id });
        }

        public IActionResult Confirmation(int rentalId)
        {
            var rental = _context.Rentals.Find(rentalId);
            if (rental == null)
            {
                return NotFound("Zamówienie nie istnieje.");
            }

            return View(rental);
        }

    // GET: Rental
    public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals.Include(r => r.Car).Include(r => r.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rental/Create
        public IActionResult Create(int clientId, int carId)
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand");
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,CarId,RentalStart,RentalEnd,Price")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                rental.Client = await _userManager.FindByIdAsync(rental.ClientId);
                rental.Car = await _context.Cars.FindAsync(rental.CarId);

                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", rental.Car);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", rental.Client);
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", rental.CarId);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", rental.ClientId);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,CarId,RentalStart,RentalEnd,Price")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", rental.CarId);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", rental.ClientId);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
