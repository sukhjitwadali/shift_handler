using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shifthandler.Data;
using shifthandler.Models;
using System;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.Rendering; // For SelectList
using Microsoft.AspNetCore.Http; // For CookieOptions

namespace shifthandler.Controllers
{
    public class InvitationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvitationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invitations
        public IActionResult Index()
        {
            var invitations = _context.Invitations
                .Include(i => i.Shift)
                .Include(i => i.Worker)
                .ToList();
            return View(invitations);
        }

        // GET: Invitations/Create
        public IActionResult Create()
        {
            ViewBag.ShiftId = new SelectList(_context.Shifts, "Id", "Location");
            ViewBag.WorkerId = new SelectList(_context.Workers, "Id", "Name");
            return View();
        }

        // POST: Invitations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ShiftId,WorkerId")] Invitation invitation)
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
                invitation.InvitationDate = DateTime.Now;
                invitation.ConfirmationGuid = Guid.NewGuid();
                _context.Invitations.Add(invitation);
                _context.SaveChanges();

                SendInvitationEmail(invitation);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ShiftId = new SelectList(_context.Shifts, "Id", "Location", invitation.ShiftId);
            ViewBag.WorkerId = new SelectList(_context.Workers, "Id", "Name", invitation.WorkerId);
            return View(invitation);
        }

        public IActionResult Confirm(Guid id)
        {
            var invitation = _context.Invitations.SingleOrDefault(i => i.ConfirmationGuid == id);
            if (invitation == null)
            {
                return NotFound();
            }

            invitation.ConfirmationDate = DateTime.Now;
            _context.SaveChanges();

            return View("ConfirmationSuccess");
        }

        private void SendInvitationEmail(Invitation invitation)
        {
            var worker = _context.Workers.Find(invitation.WorkerId);
            var shift = _context.Shifts.Find(invitation.ShiftId);
            var confirmationLink = Url.Action("Confirm", "Invitations", new { id = invitation.ConfirmationGuid }, protocol: HttpContext.Request.Scheme);

            if (worker != null && shift != null)
            {
                try
                {
                    Console.WriteLine($"before", worker.Email);
                    var mailMessage = new MailMessage("anything@pkyz.imitate.email", worker.Email)
                    {
                        Subject = "Shift Invitation",
                        Body = $"You are invited to a shift on {shift.Date} at {shift.Location}. Click <a href='{confirmationLink}'>here</a> to confirm.",
                        IsBodyHtml = true
                    };
                    Console.WriteLine($"after", mailMessage);
                    using (var smtpClient = new SmtpClient("smtp.imitate.email"))
                    {
                        smtpClient.Port = 587; // Set your SMTP port
                        smtpClient.Credentials = new System.Net.NetworkCredential("bIhA-6xVa0yM-AGPzK427w", "0HvO0H0T6ZSsiVfCueFn");
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mailMessage);
                    }
                    Console.WriteLine("end");
                }
                catch (SmtpException smtpEx)
                {
                    // Handle SMTP specific exceptions
                    Console.WriteLine($"SMTP error: {smtpEx}");
                }
                catch (FormatException formatEx)
                {
                    // Handle email format exceptions
                    Console.WriteLine($"Email format error: {formatEx.Message}");
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine($"General error: {ex.Message}");
                }
            }
        }

        private bool InvitationExists(int id)
        {
            return _context.Invitations.Any(e => e.Id == id);
        }
    }
}
