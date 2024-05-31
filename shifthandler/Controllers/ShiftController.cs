using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shifthandler.Data;
using shifthandler.Models;
using System.Linq;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Logging;


namespace shifthandler.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ShiftController> _logger; // Declare a logger field

        public ShiftController(ApplicationDbContext context, ILogger<ShiftController> logger)
        {
            _context = context;
            _logger = logger;

        }


        public IActionResult Index()
        {
            return View(_context.Shifts.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Date,Time,Location,Task,Rate")] Shifts shift)
        {
            Response.Cookies.Append("MyCookieName", "cookieValue", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(1), // Set expiration time as needed
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = false
            });
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Create action called.");
                _context.Shifts.Add(shift);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(shift);
        }


        // Additional actions (Edit, Details, Delete) would follow the same pattern
    }
}
