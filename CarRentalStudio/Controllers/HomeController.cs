using CarRentalStudio.Data;
using CarRentalStudio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace CarRentalStudio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var ratings = _context.Rating
            .Include(r => r.User) // £adowanie danych u¿ytkownika
            .ToList();
            return View(ratings);
            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRating(Rating rating)
        {
            // SprawdŸ, czy u¿ytkownik jest zalogowany
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Musisz siê zalogowaæ, aby dodaæ opiniê.";

                return Redirect("Index#formularz-opinii");
            }

            if (ModelState.IsValid)
            {
                // Pobierz ID zalogowanego u¿ytkownika
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Unauthorized(); // Brak zalogowanego u¿ytkownika
                }

                // Pobierz pe³ny obiekt u¿ytkownika z bazy
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    return Unauthorized(); // U¿ytkownik nie istnieje w bazie
                }

                // Przypisz u¿ytkownika do opinii
                rating.UserId = user.Id; // Klucz obcy
                rating.User = user;     // Opcjonalnie, jeœli potrzebujesz pe³nego obiektu w przysz³oœci

                // Dodaj opiniê do bazy danych
                _context.Rating.Add(rating);
                _context.SaveChanges();

                return Redirect("Index#formularz-opinii");
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                // Jeœli ModelState nie jest poprawny, wyœwietl b³êdy
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View("Index");
            }

           // return View("Index");
        }

        public IActionResult HowItWorks()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
