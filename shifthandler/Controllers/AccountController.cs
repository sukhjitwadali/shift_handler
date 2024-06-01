using Microsoft.AspNetCore.Mvc;
using shifthandler.Models;

namespace shifthandler.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Simulate checking credentials
                // In a real application, replace this with your actual authentication logic
                if (model.Email == "sukhjit@gmail.com" && model.Password == "pass123")
                {
                    // Authentication successful
                    // Redirect to a secure area
                    return RedirectToAction("Index", "Shift");
                }
                else
                {
                    // Authentication failed
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            // If we got this far, something failed; redisplay form
            return View(model);
        }
    }
}