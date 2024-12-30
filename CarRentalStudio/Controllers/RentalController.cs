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
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalStudio.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EmailService _emailService;
        public RentalController(ApplicationDbContext context, UserManager<IdentityUser> userManager, EmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> OrderConfirmation(string selectedDates, int carId)
        {
            // selectedDates zawiera wybrane daty w formacie "YYYY-MM-DD to YYYY-MM-DD"
            var dates = selectedDates.Split(" to "); // Rozdzielenie zakresu dat
            DateTime startDate = DateTime.Parse(dates[0]);
            DateTime endDate = DateTime.Parse(dates[1]);

            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                return NotFound("Nie znaleziono samochodu.");
            }

            int rentalDays = (endDate - startDate).Days;
            decimal totalPrice = rentalDays * car.DailyRate;

            var viewModel = new ConfirmOrderViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                CarId = car.Id,
                CarBrand = car.Brand,
                CarModel = car.Model,
                PricePerDay = car.DailyRate,
                TotalPrice = totalPrice
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeOrder(ConfirmOrderViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (user == null)
            {
                return Unauthorized("Nie znaleziono użytkownika.");
            }

            var rental = new Rental
            {
                CarId = model.CarId,
                Car = await _context.Cars.FindAsync(model.CarId),
                RentalStart = model.StartDate,
                RentalEnd = model.EndDate,
                ClientId = user.Id,
                Client = user,
            };

            rental.Price = rental.CalculatePrice();

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation", new { rentalId = rental.Id });
        }
        public IActionResult Confirmation()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals.Include(r => r.Car).Include(r => r.Client);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        // GET: Rental/Create
        public IActionResult Create(int clientId, int carId)
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand");
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        [Authorize(Roles = "Admin")]
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

                if (rental.Client == null || rental.Car == null)
                {
                    ModelState.AddModelError("", "Nie znaleziono klienta lub samochodu.");
                    ViewData["Clients"] = new SelectList(_context.Users, "Id", "UserName", rental.ClientId);
                    ViewData["Cars"] = new SelectList(_context.Cars, "Id", "Model", rental.CarId);
                    return View(rental);
                }

                rental.Price = rental.CalculatePrice();

                _context.Add(rental);
                await _context.SaveChangesAsync();
                string emailBody = $@"
                <h1>Rental confirmation</h1>
                <p>Thank you for renting our car!</p>
                <p><strong>Car:</strong> {rental.Car.Brand}</p>
                <p><strong>Start Date:</strong> {rental.RentalStart}</p>
                <p><strong>End Date:</strong> {rental.RentalEnd}</p>
                <p><strong>Price:</strong> {rental.Price:C}</p>";

                // Wysyłanie e-maila
                //await _emailService.SendEmailAsync(client.Email, "Rental Confirmation", emailBody);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", rental.Car);
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "UserName", rental.Client);

            return View(rental);
        }
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize(Roles = "Admin,Manager")]
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
        [Authorize]
        public async Task<IActionResult> RentalHistory()
        {
            // Pobierz zalogowanego użytkownika
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Pobierz historię wypożyczeń dla zalogowanego użytkownika
            var rentals = await _context.Rentals
                .Where(r => r.ClientId == user.Id) 
                .Include(r => r.Car) 
                .ToListAsync();

            return View(rentals);
        }

    }
}
