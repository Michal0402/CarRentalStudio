using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarRentalStudio.Models;
using CarRentalStudio.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;

namespace CarRentalStudio.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(
         UserManager<IdentityUser> userManager,
         RoleManager<IdentityRole> roleManager,
         ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
            Console.WriteLine("Renderowanie widoku _CarCreate.");
            return PartialView("_CarCreate");
        }
        // Dodanie nowego samochodu (POST)
        [HttpPost]
        public async Task<IActionResult> CreateCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                // Po dodaniu samochodu przeładuj listę samochodów w panelu admina
                return RedirectToAction("Cars");
            }
            return PartialView("_CarCreate", car);
        }
        // Edycja samochodu (GET)
        public async Task<IActionResult> EditCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return PartialView("_CarEdit", car);
        }
        // Edycja samochodu (POST)
        [HttpPost]
        public async Task<IActionResult> EditCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
                var cars = await _context.Cars.ToListAsync();
                return PartialView("_CarList", cars);
            }
            return PartialView("_CarEdit", car);
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

            var cars = await _context.Cars.ToListAsync();
            return PartialView("_CarList", cars);
        }
        // wyświetlenie użytkowników
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return PartialView("_UserList", users);
        }
        // Dodanie użytkownika (GET)
        public async Task<IActionResult> CreateUser()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.Roles = roles; // Przekaż role do widoku
            return PartialView("_UserCreate");
        }
        //Dodanie użytkownika (POST)
        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                ModelState.AddModelError("", "All fields are required.");
                return PartialView("_UserCreate");
            }

            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            // Tworzenie użytkownika
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return PartialView("_UserCreate");
            }

            // Przypisanie roli
            if (!await _roleManager.RoleExistsAsync(role))
            {
                ModelState.AddModelError("", "Role does not exist.");
                return PartialView("_UserCreate");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return PartialView("_UserCreate");
            }

            // Jeśli wszystko się udało, przeładuj listę użytkowników
            var users = await _userManager.Users.ToListAsync();
            return PartialView("_UserList", users);
        }
        // Edycja użytkownika (GET)
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.Roles = roles;

            return PartialView("_UserEdit", user);
        }
        //Edycja użytkownika (POST)
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, IdentityUser updatedUser, string role)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                // Aktualizuj dane użytkownika
                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update user.");
                    var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                    ViewBag.Roles = roles;
                    return PartialView("_UserEdit", updatedUser);
                }

                // Aktualizacja roli użytkownika
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                }

                if (!string.IsNullOrEmpty(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }

                // Pobierz zaktualizowaną listę użytkowników
                var users = await _userManager.Users.ToListAsync();
                return PartialView("_UserList", users);
            }

            var availableRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.Roles = availableRoles;

            return PartialView("_UserEdit", updatedUser);
        }
        //Usunięcie użytkownika (GET)
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            var users = await _context.Users.ToListAsync();
            return PartialView("_UserList", users);

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
