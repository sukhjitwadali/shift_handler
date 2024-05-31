using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shifthandler.Data;
using shifthandler.Models;
using System.Linq;

namespace shifthandler.Controllers
{
    public class WorkersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Workers.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email,Phone,Position")] Worker worker)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                // Other cookie options
            };
            if (ModelState.IsValid)
            {
                _context.Workers.Add(worker);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(worker);
        }

        // Additional actions (Edit, Details, Delete) would follow the same pattern
    }
}
