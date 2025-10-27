using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet_Adoption_Forum.Data;
using Pet_Adoption_Forum.Models;

namespace Pet_Adoption_Forum.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets.ToListAsync();
            return View(pets);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null) return NotFound();

            return View(pet);
        }

        public async Task<IActionResult> Adopt(int? id)
        {
            if (id == null) return NotFound();

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();

            return View(new AdoptionRequest { PetId = pet.Id, PetName = pet.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adopt(AdoptionRequest request)
        {
            if (ModelState.IsValid)
            {
                request.RequestDate = DateTime.Now;
                request.Status = "Pending";

                _context.AdoptionRequests.Add(request);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Adoption request submitted!";
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }
    }
}
