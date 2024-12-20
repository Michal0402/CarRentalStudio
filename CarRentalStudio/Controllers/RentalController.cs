﻿using System;
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

        // Akcja do wyświetlania formularza potwierdzenia
        [HttpGet]
        public IActionResult ConfirmOrder(int carId, DateTime rentalStart, DateTime rentalEnd)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null)
            {
                return NotFound("Samochód nie został znaleziony");
            }

            var rental = new Rental
            {
                CarId = carId,
                Car = car,
                RentalStart = rentalStart,
                RentalEnd = rentalEnd,
                ClientId = User.Identity.Name // Załóżmy, że klient jest już zalogowany
            };

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

            return View(new { CarId = carId, Car = car });
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
            bool isAvailable = _context.Rentals.Any(r => r.CarId == carId &&
                ((startDate >= r.RentalStart && startDate <= r.RentalEnd) ||
                 (endDate >= r.RentalStart && endDate <= r.RentalEnd) ||
                 (startDate <= r.RentalStart && endDate >= r.RentalEnd)));

            if (!isAvailable)
            {
                return Json(new { available = false, message = "Samochód jest zajęty w wybranym terminie." });
            }

            return Json(new { available = true, redirectUrl = Url.Action("ConfirmOrder", new { carId, rentalStart = startDate, rentalEnd = endDate }) });
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
                var client = await _userManager.FindByIdAsync(rental.ClientId);
                var car = await _context.Cars.FindAsync(rental.CarId);

                if(car != null)
                {
                    rental.Car = car;
                    rental.Client = client;
                }

                _context.Add(rental);
                await _context.SaveChangesAsync();
                string emailBody = $@"
                <h1>Rental confirmation</h1>
                <p>Thank you for renting our car!</p>
                <p><strong>Car:</strong> {car.Brand}</p>
                <p><strong>Start Date:</strong> {rental.RentalStart}</p>
                <p><strong>End Date:</strong> {rental.RentalEnd}</p>
                <p><strong>Price:</strong> {rental.Price:C}</p>";

                // Wysyłanie e-maila
                //await _emailService.SendEmailAsync(client.Email, "Rental Confirmation", emailBody);
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
