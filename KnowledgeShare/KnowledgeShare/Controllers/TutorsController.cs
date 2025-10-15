using KnowledgeShare.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeShare.Controllers
{
    public class TutorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var tutorsQuery = _context.Users.Where(u => u.Role == "Tutor");

            if (!string.IsNullOrEmpty(searchString))
            {
                var tutorIdsWithSubject = await _context.Subjects
                    .Where(s => s.Name.ToLower().Contains(searchString.ToLower()))
                    .Select(s => s.TutorId)
                    .Distinct()
                    .ToListAsync();

                tutorsQuery = tutorsQuery.Where(t => tutorIdsWithSubject.Contains(t.Id));
            }

            var tutors = await tutorsQuery.ToListAsync();

            ViewBag.Subjects = await _context.Subjects.ToListAsync();

            return View(tutors);
        }
    }
}
