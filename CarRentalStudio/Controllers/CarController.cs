using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalStudio.Data;
using CarRentalStudio.Models;

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
            ViewBag.CarId = id;
            var Car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
                
            return View(Car);
        }

        public async Task<IActionResult> CarsMainPanel()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }

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

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Brand,Model,Year,Mileage,HorsePower, Torque, EngineCapacity, FuelType, Transmission, BodyType, DailyRate,Image")] Car car)
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

            return View(car);
        }

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

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Brand,Model,Year,Mileage,HorsePower, Torque, EngineCapacity, FuelType, Transmission, BodyType, DailyRate,Image")] Car car)
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
