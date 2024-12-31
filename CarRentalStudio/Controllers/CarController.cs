using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalStudio.Data;
using CarRentalStudio.Models;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalStudio.Controllers
{
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetUnavailableDates(int carId)
        {
            var car = _context.Cars.Include(c => c.Rentals).FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return NotFound();
            }

            // Lista zablokowanych dat
            var unavailableDates = car.Rentals.Select(r => new
            {
                from = r.RentalStart.ToString("yyyy-MM-dd"), // Data rozpoczęcia
                to = r.RentalEnd.ToString("yyyy-MM-dd")     // Data zakończenia
            }).ToList();

            return Json(unavailableDates);
        }
        // GET: Car/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.CarId = id;
            var Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
                
            return View(Car);
        }

        public async Task<IActionResult> CarsMainPanel(decimal? minPrice, decimal? maxPrice, List<string> brands)
        {
            var carsQuery = _context.Cars.AsQueryable();

            // Filtrowanie po marce
            if (brands != null && brands.Any())
            {
                carsQuery = carsQuery.Where(c => brands.Contains(c.Brand));
            }

            // Filtrowanie po cenie
            if (minPrice.HasValue)
            {
                carsQuery = carsQuery.Where(c => c.DailyRate >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                carsQuery = carsQuery.Where(c => c.DailyRate <= maxPrice.Value);
            }

            var cars = await carsQuery.ToListAsync();
            // Pobierz wszystkie unikalne marki do ViewBag
            var allBrands = await _context.Cars.Select(c => c.Brand).Distinct().ToListAsync();
            ViewBag.Brands = allBrands;
            return View(cars);
        }

        [Authorize(Roles = "Admin")]
        // GET: Car
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        // GET: Car/Create
        public IActionResult Create()
        {
            ViewBag.FuelTypes = Enum.GetValues(typeof(FuelType))
            .Cast<FuelType>()
            .Select(ft => new SelectListItem
            {
                Text = ft.ToString(),
                Value = ft.ToString()
            });

            ViewBag.TransmissionTypes = Enum.GetValues(typeof(TransmissionType))
                .Cast<TransmissionType>()
                .Select(tt => new SelectListItem
                {
                    Text = tt.ToString(),
                    Value = tt.ToString()
                });

            ViewBag.BodyTypes = Enum.GetValues(typeof(BodyType))
                .Cast<BodyType>()
                .Select(bt => new SelectListItem
                {
                    Text = bt.ToString(),
                    Value = bt.ToString()
                });

            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Brand,Model,Year,Mileage,HorsePower, Torque, EngineCapacity, FuelType, Transmission, BodyType, Drive, Acceleration, VMax, DailyRate,Image")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FuelTypes = Enum.GetValues(typeof(FuelType))
            .Cast<FuelType>()
            .Select(ft => new SelectListItem
            {
                Text = ft.ToString(),
                Value = ft.ToString()
            });

            ViewBag.TransmissionTypes = Enum.GetValues(typeof(TransmissionType))
                .Cast<TransmissionType>()
                .Select(tt => new SelectListItem
                {
                    Text = tt.ToString(),
                    Value = tt.ToString()
                });

            ViewBag.BodyTypes = Enum.GetValues(typeof(BodyType))
                .Cast<BodyType>()
                .Select(bt => new SelectListItem
                {
                    Text = bt.ToString(),
                    Value = bt.ToString()
                });

            ViewBag.FuelTypes = Enum.GetValues(typeof(Drive))
            .Cast<Drive>()
            .Select(ft => new SelectListItem
            {
                Text = ft.ToString(),
                Value = ft.ToString()
            });

            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Brand,Model,Year,Mileage,HorsePower, Torque, EngineCapacity, FuelType, Transmission, BodyType, Drive, Acceleration, VMax, DailyRate,Image")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
