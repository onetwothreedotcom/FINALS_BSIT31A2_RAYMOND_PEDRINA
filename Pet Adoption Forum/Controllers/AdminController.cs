using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Adoption_Forum.Data;
using Pet_Adoption_Forum.Models;

namespace Pet_Adoption_Forum.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Dashboard");
            }
            ModelState.AddModelError("", "Invalid login");
            return View();
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var requests = await _context.AdoptionRequests
                .Where(r => r.Status == "Pending")
                .Include(r => r.Pet)
                .ToListAsync();

            var availablePets = await _context.Pets.Where(p => !p.IsAdopted).ToListAsync();
            var adoptedPets = await _context.Pets.Where(p => p.IsAdopted).ToListAsync();

            ViewBag.AvailablePets = availablePets;
            ViewBag.AdoptedPets = adoptedPets;

            return View(requests);
        }

        public IActionResult AddPet()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPet(Pet pet)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(pet);
        }

        public async Task<IActionResult> EditPet(int? id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            if (id == null) return NotFound();

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();

            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(int id, Pet pet)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            if (id != pet.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveAdoption(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var request = await _context.AdoptionRequests
                .Include(r => r.Pet)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return NotFound();

            request.Status = "Approved";
            request.IsSelected = true;
            request.Pet.IsAdopted = true;

            // Reject other pending requests for the same pet
            var otherRequests = await _context.AdoptionRequests
                .Where(r => r.PetId == request.PetId && r.Id != id && r.Status == "Pending")
                .ToListAsync();

            foreach (var req in otherRequests)
            {
                req.Status = "Rejected";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
