using E_LearningProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this for logging
using System.Threading.Tasks; // Add this for async operations

namespace E_LearningProject.Controllers
{
    [Authorize]
    public class CouerseController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly ILogger<CouerseController> _logger; // Use correct class name here

        public CouerseController(ApplicationDBContext db, ILogger<CouerseController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var courses = await _db.Courses
                    .Include(c => c.category) // Assuming your navigation property is "category"
                    .ToListAsync();
                return View("Details", courses); // Changed here
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching courses: {Message}", ex.Message);
                return View("Error");
            }
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var course = await _db.Courses
                    .Include(c => c.category)
                    .FirstOrDefaultAsync(c => c.CourseId == id);

                if (course == null)
                {
                    return NotFound();
                }

                return View("CourseInfo", course); // Use the correct view name here
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching course details: {Message}", ex.Message);
                return View("Error");
            }
        }


        // ... (Other actions: Create, Edit, Delete - you'll need to fill in the actual implementation) ...
    }
}
