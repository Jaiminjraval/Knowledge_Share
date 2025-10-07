using KnowledgeShare.Data;
using KnowledgeShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeShare.Controllers
{
    [Authorize] // All actions in this controller require login
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound();
            }

            ViewBag.MySubjects = await _context.Subjects
                .Where(s => s.TutorId == currentUser.Id)
                .ToListAsync();

            return View(currentUser);
        }

        // POST: Profile/EditBio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBio(string bio)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.Bio = bio;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Profile/AddSubject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubject(string subjectName)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Role == "Tutor" && !string.IsNullOrWhiteSpace(subjectName))
            {
                var subject = new Subject { Name = subjectName, TutorId = user.Id };
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Profile/RemoveSubject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSubject(int subjectId)
        {
            var user = await _userManager.GetUserAsync(User);
            var subject = await _context.Subjects.FindAsync(subjectId);

            // Security check: ensure the user owns this subject
            if (user != null && subject != null && subject.TutorId == user.Id)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
