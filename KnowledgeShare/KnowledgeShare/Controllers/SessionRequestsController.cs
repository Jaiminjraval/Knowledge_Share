using KnowledgeShare.Data;
using KnowledgeShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeShare.Controllers
{
    [Authorize]
    public class SessionRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SessionRequestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: SessionRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string tutorId)
        {
            var learner = await _userManager.GetUserAsync(User);
            if (learner.Role != "Learner")
            {
                TempData["ErrorMessage"] = "Only learners can request sessions.";
                return RedirectToAction("Index", "Tutors");
            }

            var existingRequest = await _context.SessionRequests
                .FirstOrDefaultAsync(r => r.LearnerId == learner.Id && r.TutorId == tutorId && r.Status == RequestStatus.Pending);

            if (existingRequest != null)
            {
                TempData["ErrorMessage"] = "You already have a pending request with this tutor.";
                return RedirectToAction("Index", "Tutors");
            }

            var request = new SessionRequest
            {
                LearnerId = learner.Id,
                TutorId = tutorId,
                Status = RequestStatus.Pending
            };

            _context.SessionRequests.Add(request);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Session requested successfully!";
            return RedirectToAction("Index", "Tutors");
        }

        // GET: SessionRequests/MySessions (For Learners)
        public async Task<IActionResult> MySessions()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var requests = await _context.SessionRequests
                .Where(r => r.LearnerId == currentUser.Id)
                .ToListAsync();

            ViewBag.Users = await _context.Users.ToListAsync(); // For getting tutor names
            return View(requests);
        }

        // GET: SessionRequests/MyRequests (For Tutors)
        public async Task<IActionResult> MyRequests()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Role != "Tutor") return Forbid();

            var requests = await _context.SessionRequests
                .Where(r => r.TutorId == currentUser.Id)
                .ToListAsync();

            ViewBag.Users = await _context.Users.ToListAsync(); 
            return View(requests);
        }

        // POST: SessionRequests/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int requestId) => await UpdateRequestStatus(requestId, RequestStatus.Approved);

        // POST: SessionRequests/Decline
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(int requestId) => await UpdateRequestStatus(requestId, RequestStatus.Declined);

        private async Task<IActionResult> UpdateRequestStatus(int requestId, RequestStatus newStatus)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var request = await _context.SessionRequests.FindAsync(requestId);

            if (request == null || request.TutorId != currentUser.Id)
            {
                return Forbid(); 
            }

            request.Status = newStatus;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyRequests));
        }
    }
}
