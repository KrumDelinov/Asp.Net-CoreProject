using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESchool.Data;
using ESchool.Data.Models;

namespace ESchool.Web.Controllers
{
    public class CoursesTeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesTeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CoursesTeachers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CoursesTeachers.Include(c => c.Course).Include(c => c.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CoursesTeachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesTeachers = await _context.CoursesTeachers
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (coursesTeachers == null)
            {
                return NotFound();
            }

            return View(coursesTeachers);
        }

        // GET: CoursesTeachers/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName");
            return View();
        }

        // POST: CoursesTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,TeacherId,IsSelected")] CoursesTeachers coursesTeachers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursesTeachers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", coursesTeachers.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", coursesTeachers.TeacherId);
            return View(coursesTeachers);
        }

        // GET: CoursesTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesTeachers = await _context.CoursesTeachers.FindAsync(id);
            if (coursesTeachers == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", coursesTeachers.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", coursesTeachers.TeacherId);
            return View(coursesTeachers);
        }

        // POST: CoursesTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,TeacherId,IsSelected")] CoursesTeachers coursesTeachers)
        {
            if (id != coursesTeachers.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursesTeachers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesTeachersExists(coursesTeachers.TeacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", coursesTeachers.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", coursesTeachers.TeacherId);
            return View(coursesTeachers);
        }

        // GET: CoursesTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesTeachers = await _context.CoursesTeachers
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (coursesTeachers == null)
            {
                return NotFound();
            }

            return View(coursesTeachers);
        }

        // POST: CoursesTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coursesTeachers = await _context.CoursesTeachers.FindAsync(id);
            _context.CoursesTeachers.Remove(coursesTeachers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesTeachersExists(int id)
        {
            return _context.CoursesTeachers.Any(e => e.TeacherId == id);
        }
    }
}
