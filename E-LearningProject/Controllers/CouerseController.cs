using E_LearningProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningProject.Controllers
{
    public class CouerseController : Controller
    {
        // GET: CouerseController
        ApplicationDBContext db =new ApplicationDBContext();
        public ActionResult Index()
        {
            return View();
        }

        // GET: CouerseController/Details/5
        public ActionResult Details(int id)
        {
            var query = db.Courses.Include(s=>s.category);

            return View(query);
        }

        // GET: CouerseController/Create
        public ActionResult CourseInfo(int id)
        {
            var query = db.Courses.Include(x => x.category).FirstOrDefault(c=>c.CourseId==id);
            return View(query);
        }

        // POST: CouerseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CouerseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CouerseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CouerseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CouerseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
