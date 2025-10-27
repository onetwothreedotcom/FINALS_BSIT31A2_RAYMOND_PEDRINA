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
                return RedirectToAction("Dashboard");
            }
            ModelState.AddModelError("", "Invalid login");
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var requests = await _context.AdoptionRequests
                .Where(r => r.Status == "Pending")
                .Include(r => r.Pet)
                .ToListAsync();

            return View(requests);
        }

        public IActionResult AddPet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPet(Pet pet)
        {
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
            if (id == null) return NotFound();

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();

            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPet(int id, Pet pet)
        {
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
            var request = await _context.AdoptionRequests
                .Include(r => r.Pet)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return NotFound();

            request.Status = "Approved";
            request.Pet.IsAdopted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }
    }
}
