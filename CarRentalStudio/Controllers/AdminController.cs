using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CarRentalStudio.Models;
using CarRentalStudio.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRentalStudio.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
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
        // Dodanie wypożyczenia (GET)
        public IActionResult CreateRental()
        {
            ViewBag.ClientId = new SelectList(_userManager.Users.ToList(), "Id", "UserName");
            ViewBag.CarId = new SelectList(_context.Cars.ToList(), "Id", "Brand");
            return PartialView("_RentalCreate");
        }
        // Dodanie wypożyczenia (POST)
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ClientId,CarId,RentalStart,RentalEnd,Price")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                rental.Client = await _userManager.FindByIdAsync(rental.ClientId);
                rental.Car = await _context.Cars.FindAsync(rental.CarId);
                if (rental.Client == null || rental.Car == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Client or Car selection.");
                    return PartialView("_RentalCreate", rental);
                }
                _context.Add(rental);
                var rentals = await _context.Rentals.Include(r => r.Client).Include(r => r.Car).ToListAsync();
                return PartialView("_RentalList", rentals);
            }
            ViewBag.ClientId = new SelectList(_userManager.Users.ToList(), "Id", "UserName", rental.ClientId);
            ViewBag.CarId = new SelectList(_context.Cars.ToList(), "Id", "Brand", rental.CarId);
            return PartialView("_RentalList");
        }
        //Edytowanie wypożyczenia (GET)
        public async Task<IActionResult> EditRental(int id)
        {
            // Znajdź wypożyczenie w bazie danych na podstawie jego Id
            var rental = await _context.Rentals
                .Include(r => r.Client)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (rental == null)
            {
                return NotFound(); // Zwróć błąd 404, jeśli wypożyczenie nie istnieje
            }
            // Przygotuj dane do wyświetlenia w formularzu
            ViewBag.ClientId = new SelectList(_userManager.Users, "Id", "UserName", rental.ClientId);
            ViewBag.CarId = new SelectList(_context.Cars, "Id", "Brand", rental.CarId);

            return PartialView("_RentalEdit", rental); // Zwróć widok edycji
        }
        // Zapisanie zmian w wypożyczeniu (POST)
        [HttpPost]
        public async Task<IActionResult> EditRental(int id, [Bind("Id,ClientId,CarId,RentalStart,RentalEnd,Price")] Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest(); // Zwróć błąd 400, jeśli Id w modelu nie zgadza się z Id w URL
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Znajdź istniejące wypożyczenie w bazie danych
                    var existingRental = await _context.Rentals.FindAsync(id);

                    if (existingRental == null)
                    {
                        return NotFound(); // Zwróć błąd 404, jeśli wypożyczenie nie istnieje
                    }
                    // Aktualizuj właściwości wypożyczenia
                    existingRental.ClientId = rental.ClientId;
                    existingRental.CarId = rental.CarId;
                    existingRental.RentalStart = rental.RentalStart;
                    existingRental.RentalEnd = rental.RentalEnd;

                    // Zapisz zmiany w bazie danych
                    _context.Rentals.Update(existingRental);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Rentals"); // Przekierowanie do listy wypożyczeń
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
                    {
                        return NotFound(); // Jeśli wypożyczenie zostało usunięte w międzyczasie
                    }
                    else
                    {
                        throw; // Ponów wyjątek, jeśli wystąpił inny problem
                    }
                }
            }

            // Przygotuj dane do widoku w przypadku błędu
            ViewBag.ClientId = new SelectList(_userManager.Users, "Id", "UserName", rental.ClientId);
            ViewBag.CarId = new SelectList(_context.Cars, "Id", "Brand", rental.CarId);

            return PartialView("_RentalEdit", rental); // Zwróć widok edycji z błędami
        }
        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(r => r.Id == id);
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
            var rentals = await _context.Rentals.ToListAsync();
            return PartialView("_RentalList", rentals);
        }
    }
}
